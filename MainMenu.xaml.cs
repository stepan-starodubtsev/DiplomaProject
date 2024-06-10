using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiplomaProject.Entities;
using DiplomaProject.Patterns;

namespace DiplomaProject
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private ObservableCollection<Pattern> _patterns = new ObservableCollection<Pattern>();
        private UIElement _iElement;
        private User _currentUser;
        public ObservableCollection<Pattern> Patterns { get => _patterns; set => _patterns = value; }
        public UIElement IElement { get => _iElement; set => _iElement = value; }
        public User CurrentUser { get => _currentUser; set => _currentUser = value; }
        public MainMenu() { }
        public MainMenu(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            userName_textBox.Text = CurrentUser.Fullname;
            userLogin_textBox.Text = CurrentUser.Login;
            Patterns.Add(new VacationAppl(this));
            Patterns.Add(new TransferPattern(this));
            Patterns.Add(new MoneyPattern(this));
            AddPatternsToListBox();
            LoadHistory();
        }



        #region ControlMethods

        private void patterns_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadPattern(e);
        }

        private void userOut_link_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void iconButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPattern(e);
        }

        private void persons_btn_Click(object sender, RoutedEventArgs e)
        {
            PersonsPage personsPage = new PersonsPage(CurrentUser);
            personsPage.Show();
            this.Close();
        }
        #endregion

        #region MainMetods
        /// <summary>
        /// Завантажує обраний шаблон
        /// </summary>
        /// <param name="e"></param>
        private void LoadPattern(RoutedEventArgs e)
        {
            foreach (var pattern in Patterns)
            {
                if (pattern.FileName.Equals((e.Source as Button).Name))
                {
                    Grid patternGrid = pattern.PlaceElements();
                    Grid.SetColumn(patternGrid, 1);
                    Grid.SetRow(patternGrid, 1);
                    main_grid.Children.Remove(IElement);
                    main_grid.Children.Add(patternGrid);
                }
            }
        }
        /// <summary>
        /// Динамічно виводить список усіх шаблонів в робочу область
        /// </summary>
        public void AddPatternsToListBox()
        {
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Name = "patterns_wrapPanel";
            wrapPanel.Orientation = Orientation.Horizontal;
            wrapPanel.Margin = new Thickness(20);

            foreach (var pattern in Patterns)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(pattern.IconPath));
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                Image image = new Image();
                image.Source = bitmap;
                image.Name = $"{pattern.FileName}_image";

                Button button = new Button();
                button.Name = $"{pattern.FileName}";
                button.Height = 500;
                button.Width = 347;
                button.Click += new RoutedEventHandler(iconButton_Click);
                button.Content = image;

                TextBlock textBlock = new TextBlock();
                textBlock.Name = $"{pattern.FileName}_textBox";
                textBlock.Text = pattern.PatternName;
                textBlock.Margin = new Thickness(5);
                textBlock.FontSize = 16;

                StackPanel stackPanel = new StackPanel();
                stackPanel.Name = $"{pattern.FileName}_stackPanel";
                stackPanel.Children.Add(button);
                stackPanel.Children.Add(textBlock);

                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Padding = new Thickness(5);
                border.Margin = new Thickness(5);
                border.Child = stackPanel;

                wrapPanel.Children.Add(border);
            }
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Name = "patterns_scrollViewer";
            scrollViewer.Content = wrapPanel;
            Grid.SetColumn(scrollViewer, 1);
            Grid.SetRow(scrollViewer, 1);

            main_grid.Children.Add(scrollViewer);
            IElement = scrollViewer;
        }
        /// <summary>
        /// Виводить історію використаних шаблонів у вигляді списка кнопок
        /// </summary>
        public void LoadHistory()
        {
            history_stackPanel.Children.RemoveRange(0, history_stackPanel.Children.Count);
            var directory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fileName = System.IO.Path.Combine(directory, "Patterns\\Patterns.log");
            try
            {
                using (var filestream = new FileStream(fileName, FileMode.Open))
                {
                    using (var reader = new StreamReader(filestream))
                    {
                        VacationAppl vacation = new VacationAppl(this);
                        TransferPattern transferPattern = new TransferPattern(this);
                        MoneyPattern moneyPattern = new MoneyPattern(this);
                        string tmp;
                        while ((tmp = reader.ReadLine()) != null)
                        {
                            Button button = new Button();

                            switch (tmp)
                            {
                                case "VacationAppl":
                                    button.Name = vacation.FileName;
                                    button.Content = vacation.PatternName;
                                    break;
                                case "TransferPattern":
                                    button.Name = transferPattern.FileName;
                                    button.Content = transferPattern.PatternName;
                                    break;
                                case "MoneyHelpPattern":
                                    button.Name = moneyPattern.FileName;
                                    button.Content = moneyPattern.PatternName;
                                    break;
                            }

                            button.HorizontalContentAlignment = HorizontalAlignment.Left;
                            button.FontSize = 16;
                            button.FontWeight = FontWeights.DemiBold;
                            button.BorderThickness = new Thickness(0);
                            Color color = new Color();
                            color.R = 73;
                            color.G = 73;
                            color.B = 205;
                            color.A = 100;
                            button.Background = new SolidColorBrush(color);
                            button.Foreground = Brushes.White;
                            button.Margin = new Thickness(20, 5, 20, 5);
                            button.Click += new RoutedEventHandler(patterns_btn_Click);

                            history_stackPanel.Children.Add(button);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }
}
