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
        internal ObservableCollection<Pattern> Patterns { get => _patterns; set => _patterns = value; }
        public UIElement IElement { get => _iElement; set => _iElement = value; }
        public User CurrentUser { get => _currentUser; set => _currentUser = value; }
        public MainMenu() { }
        public MainMenu(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            userName_textBox.Text = CurrentUser.Fullname;
            userLogin_textBox.Text = CurrentUser.Login;
            _patterns.Add(new VacationAppl(this));
            AddPatternsToListBox();
            LoadHistory();
        }



        #region ControlMethods
        private void addPerson_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editPerson_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removePerson_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addUnit_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editUnit_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeUnit_btn_Click(object sender, RoutedEventArgs e)
        {

        }

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
                if (pattern.IconName.Equals((e.Source as Button).Name))
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
                image.Name = $"{pattern.IconName}_image";

                Button button = new Button();
                button.Name = $"{pattern.IconName}";
                button.Height = 195;
                button.Width = 125;
                button.Click += new RoutedEventHandler(iconButton_Click);
                button.Content = image;

                TextBlock textBlock = new TextBlock();
                textBlock.Name = $"{pattern.IconName}_textBox";
                textBlock.Text = pattern.PatternName;
                textBlock.Margin = new Thickness(5);
                textBlock.FontSize = 14;

                StackPanel stackPanel = new StackPanel();
                stackPanel.Name = $"{pattern.IconName}_stackPanel";
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
                        ExcursionAppl excursion = new ExcursionAppl();
                        string tmp;
                        while ((tmp = reader.ReadLine()) != null)
                        {
                            if (tmp.Equals("VacationApplication"))
                            {
                                Button button = new Button();
                                button.Name = vacation.IconName;
                                button.Content = vacation.PatternName;
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
                            else if (tmp.Equals("ExcursionApplication"))
                            {
                                Button button = new Button();
                                button.Name = excursion.IconName;
                                button.Content = excursion.Name;
                                button.HorizontalAlignment = HorizontalAlignment.Center;
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
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }
}
