using DiplomaProject.Entities;
using DiplomaProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Microsoft.Office.Interop.Word;

namespace DiplomaProject.Patterns
{
    public class MoneyPattern : Pattern
    {
        public MoneyPattern() : base()
        {

        }
        public MoneyPattern(MainMenu owner) : base(owner)
        {
            FileName = "MoneyHelpPattern";
            IconPath = $@"D:\Lessons\OOP\DiplomaProject\Images\{FileName}.png";
            PatternName = "Рапорт на виплату підйомної допомоги";
            Sourse = $@"Patterns\PatternsWord\{FileName}.docx";
            Tags.Add("bossPost");
            Tags.Add("boss");
            Tags.Add("personPost");
            Tags.Add("person");
            Tags.Add("address");
            Tags.Add("phone");
            Tags.Add("personRank");
            Tags.Add("person");

        }
        public MoneyPattern(MainMenu owner, string iconName, string iconPath, string name, string sourse, params string[] tags) : base(owner, iconName, iconPath, name, sourse, tags)
        {

        }
        private ComboBox _forWhoComboBox;
        private ComboBox _whoComboBox;
        private ComboBox _workTypeComboBox;
        private TextBox _newWorkPlaceTextBox;
        private TextBox _newWorkPostTextBox;
        private TextBox _reasonTextBox;
        private List<Person> _persons;
        #region GridMethods
        /// <summary>
        /// Розташовує елементи всередині grid
        /// </summary>
        /// <returns>Grid з потрібними елементами</returns>
        public override Grid PlaceElements()
        {
            Grid grid = new Grid();
            grid.Name = "moneyHelpPattern_grid";

            #region SetRowsAndColls
            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            cd1.Width = new GridLength(226);
            cd2.Width = new GridLength(300);
            cd3.Width = new GridLength(400);

            grid.ColumnDefinitions.Add(cd1);
            grid.ColumnDefinitions.Add(cd2);
            grid.ColumnDefinitions.Add(cd3);

            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            rd1.Height = new GridLength(56);
            rd2.Height = new GridLength(620);
            rd3.Height = new GridLength(39);

            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);
            grid.RowDefinitions.Add(rd3);
            #endregion


            #region HeaderTextBlock
            TextBlock headerBlock = new TextBlock();
            headerBlock.Name = "pattern_header";
            headerBlock.Text = Name;
            headerBlock.FontSize = 18;
            headerBlock.VerticalAlignment = VerticalAlignment.Center;
            headerBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumnSpan(headerBlock, 3);
            Grid.SetColumn(headerBlock, 0);
            Grid.SetRow(headerBlock, 0);

            grid.Children.Add(headerBlock);
            #endregion

            #region Back button
            Button back_btn = new Button();
            back_btn.Name = "back_button";
            back_btn.Content = "Назад";
            back_btn.Margin = new Thickness(10, 15, 10, 15);
            back_btn.Padding = new Thickness(5, 0, 5, 0);
            back_btn.HorizontalAlignment = HorizontalAlignment.Left;
            back_btn.Click += new RoutedEventHandler(back_btn_Click);
            Grid.SetColumn(back_btn, 0);
            Grid.SetRow(back_btn, 0);

            grid.Children.Add(back_btn);
            #endregion

            #region PatternFieldsStackPanel
            Label forWhoLabel = new Label();
            forWhoLabel.Name = "forWho_label";
            forWhoLabel.Content = "На чиє ім'я рапорт";
            forWhoLabel.HorizontalAlignment = HorizontalAlignment.Center;
            forWhoLabel.Margin = new Thickness(10);
            forWhoLabel.FontSize = 14;

            Label whoLabel = new Label();
            whoLabel.Name = "who_label";
            whoLabel.Content = "Хто пише рапорт";
            whoLabel.HorizontalAlignment = HorizontalAlignment.Center;
            whoLabel.Margin = new Thickness(10);
            whoLabel.FontSize = 14;

            StackPanel patternFieldsStackPanel = new StackPanel();
            patternFieldsStackPanel.Name = "patternFields_stackPanel";
            patternFieldsStackPanel.Orientation = Orientation.Vertical;
            patternFieldsStackPanel.Children.Add(forWhoLabel);
            patternFieldsStackPanel.Children.Add(whoLabel);

            Grid.SetColumn(patternFieldsStackPanel, 0);
            Grid.SetRow(patternFieldsStackPanel, 1);

            grid.Children.Add(patternFieldsStackPanel);
            #endregion


            #region PatternContentStackPanel
            ComboBox forWhoComboBox = new ComboBox();
            forWhoComboBox.Name = "forWho_textBox";
            forWhoComboBox.Height = 25;
            forWhoComboBox.Margin = new Thickness(12, 15, 12, 15);
            forWhoComboBox.FontSize = 14;
            forWhoComboBox.IsTextSearchCaseSensitive = true;
            forWhoComboBox.IsEditable = true;
            _persons = PersonDBService.GetAllPersons();
            forWhoComboBox.ItemsSource = LoadComboItems(_persons);
            forWhoComboBox.PreviewTextInput += new TextCompositionEventHandler(withoutNumbers_PreviewTextInput);
            _forWhoComboBox = forWhoComboBox;

            ComboBox whoComboBox = new ComboBox();
            whoComboBox.Name = "Who_textBox";
            whoComboBox.Height = 25;
            whoComboBox.Margin = new Thickness(12, 15, 12, 15);
            whoComboBox.FontSize = 14;
            whoComboBox.IsEditable = true;
            whoComboBox.IsTextSearchCaseSensitive = true;
            whoComboBox.ItemsSource = LoadComboItems(_persons);
            whoComboBox.PreviewTextInput += new TextCompositionEventHandler(withoutNumbers_PreviewTextInput);
            _whoComboBox = whoComboBox;

            StackPanel patternContentStackPanel = new StackPanel();
            patternFieldsStackPanel.Name = "patternContent_stackPanel";
            patternFieldsStackPanel.Orientation = Orientation.Vertical;
            patternContentStackPanel.Children.Add(forWhoComboBox);
            patternContentStackPanel.Children.Add(whoComboBox);
            Grid.SetColumn(patternContentStackPanel, 1);
            Grid.SetRow(patternContentStackPanel, 1);

            grid.Children.Add(patternContentStackPanel);
            #endregion


            GridSplitter gridSplitter = new GridSplitter();
            gridSplitter.Width = 1;
            gridSplitter.Margin = new Thickness(0, 0, 0, 10);
            gridSplitter.Background = Brushes.Black;
            Grid.SetColumn(gridSplitter, 1);
            Grid.SetRow(gridSplitter, 1);
            Grid.SetRowSpan(gridSplitter, 2);

            Button printBtn = new Button();
            printBtn.Name = "print_btn";
            printBtn.Content = "Роздрукувати";
            printBtn.Margin = new Thickness(10);
            printBtn.HorizontalAlignment = HorizontalAlignment.Left;
            printBtn.Padding = new Thickness(5, 0, 5, 0);
            printBtn.Click += new RoutedEventHandler(print_btn_Click);
            Grid.SetColumn(printBtn, 1);
            Grid.SetRow(printBtn, 2);

            Button saveBtn = new Button();
            saveBtn.Name = "save_btn";
            saveBtn.Content = "Зберегти";
            saveBtn.Margin = new Thickness(10);
            saveBtn.HorizontalAlignment = HorizontalAlignment.Right;
            saveBtn.Padding = new Thickness(5, 0, 5, 0);
            saveBtn.Click += new RoutedEventHandler(save_btn_Click);
            Grid.SetColumn(saveBtn, 1);
            Grid.SetRow(saveBtn, 2);

            BitmapImage bitmap = new BitmapImage(new Uri(IconPath));
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            Image patternImage = new Image();
            patternImage.Name = $"{FileName}_image";
            patternImage.Source = bitmap;
            patternImage.Width = 300;
            patternImage.Height = 600;
            patternImage.Margin = new Thickness(20, 10, 0, 10);
            Grid.SetColumn(patternImage, 2);
            Grid.SetRow(patternImage, 1);
            Grid.SetRowSpan(patternImage, 2);

            grid.Children.Add(gridSplitter);
            grid.Children.Add(printBtn);
            grid.Children.Add(saveBtn);
            grid.Children.Add(patternImage);

            Grid = grid;
            return grid;
        }
        private void withoutNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short value;

            if (Int16.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }
        private void withoutText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short value;

            if (!Int16.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }

        private void withoutSpace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            AreaOwner.main_grid.Children.Remove(Grid);
            AreaOwner.AddPatternsToListBox();
            AreaOwner.LoadHistory();
        }
        private void print_btn_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument();
        }
        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            SaveDocument();
        }
        #endregion
        #region MainMetods
        /// <summary>
        /// Віддає список повних імен людей для передачі їх у ComboBox
        /// </summary>
        /// <param name="people"></param>
        /// <returns>Віддає список повних імен людей</returns>
        private List<string> LoadComboItems(List<Person> people)
        {
            List<string> tmp = new List<string>();
            foreach (var person in people)
            {
                tmp.Add(person.Fullname);
            }
            return tmp;
        }

        public override void FixDocumentTags(Document document)
        {
            Person boss = PersonDBService.GetPersonByFullname(_forWhoComboBox.Text);
            string tmpText = boss.Post + " " + boss.Rank;
            FixTag($"<!{Tags[0]}>", tmpText, document);
            FixTag($"<!{Tags[1]}>", boss.Fullname, document);

            Person person = PersonDBService.GetPersonByFullname(_whoComboBox.Text);
            tmpText = person.Post + " " + person.Rank;
            FixTag($"<!{Tags[2]}>", tmpText, document);
            FixTag($"<!{Tags[3]}>", person.Fullname, document);
            FixTag($"<!{Tags[4]}>", person.Adress, document);
            FixTag($"<!{Tags[5]}>", person.Phone, document);
            FixTag($"<!{Tags[6]}>", person.Rank, document);
            FixTag($"<!{Tags[3]}>", person.Fullname, document);
            
        }
        #endregion
    }
}
