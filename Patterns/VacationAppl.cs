using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;

namespace DiplomaProject
{
    public class VacationAppl : Pattern
    {
        public VacationAppl() : base()
        {

        }
        public VacationAppl(MainMenu owner) : base(owner)
        { 
            IconName = "VacationApplication";
            IconPath = @"D:\Lessons\OOP\DiplomaProject\Images\VacationAppl.jpg";
            PatternName = "Заява про відпустку";
            Sourse = @"Patterns\PatternsWord/VacationAppl.docx";
            Tags.Add("bossPost");
            Tags.Add("boss");
            Tags.Add("personPost");
            Tags.Add("person");
            Tags.Add("days");
            Tags.Add("from");
        }
        public VacationAppl(MainMenu owner, string iconName, string iconPath, string name, string sourse, params string[] tags) : base(owner, iconName, iconPath, name, sourse, tags)
        {

        }
        private ComboBox _forWhoComboBox;
        private ComboBox _whoComboBox;
        private DatePicker _fromDatePicker;
        private DatePicker _toDatePicker;
        private List<Person> _persons;
        #region GridMethods
        /// <summary>
        /// Розташовує елементи всередині grid
        /// </summary>
        /// <returns>Grid з потрібними елементами</returns>
        public override Grid PlaceElements()
        {
            Grid grid = new Grid();
            grid.Name = "VacationApplication_grid";

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
            forWhoLabel.Content = "На чиє ім'я";
            forWhoLabel.HorizontalAlignment = HorizontalAlignment.Center;
            forWhoLabel.Margin = new Thickness(10);
            forWhoLabel.FontSize = 14;

            Label WhoLabel = new Label();
            WhoLabel.Name = "Who_label";
            WhoLabel.Content = "Хто";
            WhoLabel.HorizontalAlignment = HorizontalAlignment.Center;
            WhoLabel.Margin = new Thickness(10);
            WhoLabel.FontSize = 14;

            Label fromDateLabel = new Label();
            fromDateLabel.Name = "fromDate_label";
            fromDateLabel.Content = "З якої дати";
            fromDateLabel.HorizontalAlignment = HorizontalAlignment.Center;
            fromDateLabel.Margin = new Thickness(10);
            fromDateLabel.FontSize = 14;

            Label toDateLabel = new Label();
            toDateLabel.Name = "toDate_label";
            toDateLabel.Content = "До якої дати";
            toDateLabel.HorizontalAlignment = HorizontalAlignment.Center;
            toDateLabel.Margin = new Thickness(10);
            toDateLabel.FontSize = 14;

            StackPanel patternFieldsStackPanel = new StackPanel();
            patternFieldsStackPanel.Name = "patternFields_stackPanel";
            patternFieldsStackPanel.Orientation = Orientation.Vertical;
            patternFieldsStackPanel.Children.Add(forWhoLabel);
            patternFieldsStackPanel.Children.Add(WhoLabel);
            patternFieldsStackPanel.Children.Add(fromDateLabel);
            patternFieldsStackPanel.Children.Add(toDateLabel);
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
            _persons = GetPersons($"SELECT * FROM person");
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

            DatePicker fromDatePicker = new DatePicker();
            fromDatePicker.Name = "fromDate_datePicker";
            fromDatePicker.Margin = new Thickness(12);
            fromDatePicker.SelectedDate = DateTime.Now;
            _fromDatePicker = fromDatePicker;

            DatePicker toDatePicker = new DatePicker();
            toDatePicker.Name = "toDate_datePicker";
            toDatePicker.Margin = new Thickness(12);
            toDatePicker.SelectedDate = DateTime.Now;
            _toDatePicker = toDatePicker;

            StackPanel patternContentStackPanel = new StackPanel();
            patternFieldsStackPanel.Name = "patternContent_stackPanel";
            patternFieldsStackPanel.Orientation = Orientation.Vertical;
            patternContentStackPanel.Children.Add(forWhoComboBox);
            patternContentStackPanel.Children.Add(whoComboBox);
            patternContentStackPanel.Children.Add(fromDatePicker);
            patternContentStackPanel.Children.Add(toDatePicker);
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
            patternImage.Name = $"{IconName}_image";
            patternImage.Source = bitmap;
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
        /// <summary>
        /// Заміняє тег на потрібну строку
        /// </summary>
        /// <param name="tag">Потрібний тег</param>
        /// <param name="text">Текст, на який замінять тег</param>
        /// <param name="document">об'єкт типу Word.Document</param>
        public override void FixTag(string tag, string text, Word.Document document)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: tag, ReplaceWith: text);
        }


        public override void PrintDocument()
        {
            var application = new Word.Application();
            application.Visible = false;
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var originalFileName = Path.Combine(directory, "Patterns\\PatternsWord\\VacationAppl.docx");
            var tempFileName = Path.Combine(directory, "Patterns\\PatternsWord\\VacationApplTemp.docx");
            File.Copy(originalFileName, tempFileName);
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            try
            {
                fromDate = (DateTime)_fromDatePicker.SelectedDate;
            }
            catch (FormatException)
            {
                _fromDatePicker.Text = "";
                Console.WriteLine("Неправильний формат дати у полі 'З якої дати'!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Виявлено помилку!\n{e.Message}");
            }

            try
            {
                toDate = (DateTime)_toDatePicker.SelectedDate;
            }
            catch (FormatException)
            {
                _fromDatePicker.Text = "";
                Console.WriteLine("Неправильний формат дати у полі 'До якої дати'!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Виявлено помилку!\n{e.Message}");
            }
            int days = (toDate - fromDate).Days;
            string from = $"{fromDate.Day}.{fromDate.Month}.{fromDate.Year}";
            
            var document = application.Documents.Open(tempFileName);

            List<Person> boss = GetPersons($"SELECT * FROM person WHERE fullname LIKE N'{_forWhoComboBox.Text}%'");
            string tmpText = boss[0].Post + "\n" + boss[0].Rank;
            FixTag($"<!{Tags[0]}>", tmpText, document);
            FixTag($"<!{Tags[1]}>", boss[0].Fullname, document);

            List<Person> person = GetPersons($"SELECT * FROM person WHERE fullname LIKE N'{_whoComboBox.Text}%'");
            tmpText = boss[0].Post + "\n" + boss[0].Rank;
            FixTag($"<!{Tags[2]}>", tmpText, document);
            FixTag($"<!{Tags[3]}>", person[0].Fullname, document);

            FixTag($"<!{Tags[4]}>", days.ToString(), document);
            FixTag($"<!{Tags[5]}>", from, document);

            try
            {
                application.ActiveDocument.PrintOut(true, false, Word.WdPrintOutRange.wdPrintAllDocument,
                                                    Item: Word.WdPrintOutItem.wdPrintDocumentContent, Copies: "1", Pages: "",
                                                    PageType: Word.WdPrintOutPages.wdPrintAllPages, PrintToFile: false, Collate: true,
                                                    ManualDuplexPrint: false);
                CreateLog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            application.ActiveDocument.Close();
            File.Delete(tempFileName);
            application.Quit();
        }

        public override void SaveDocument()
        {
            DateTime fromDate = new DateTime();
            DateTime toDate = new DateTime();
            try
            {
                fromDate = (DateTime)_fromDatePicker.SelectedDate;
            }
            catch (FormatException)
            {
                _fromDatePicker.Text = "";
                Console.WriteLine("Неправильний формат дати у полі 'З якої дати'!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Виявлено помилку!\n{e.Message}");
            }

            try
            {
                toDate = (DateTime)_toDatePicker.SelectedDate;
            }
            catch (FormatException)
            {
                _fromDatePicker.Text = "";
                Console.WriteLine("Неправильний формат дати у полі 'До якої дати'!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Виявлено помилку!\n{e.Message}");
            }
            int days = (toDate - fromDate).Days;
            string from = $"{fromDate.Day}.{fromDate.Month}.{fromDate.Year}";

            Word.Application application = new Word.Application();
            application.Visible = false;
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var originalFileName = Path.Combine(directory, "Patterns\\PatternsWord\\VacationAppl.docx");
            var tempFileName = Path.Combine(directory, "Patterns\\PatternsWord\\VacationApplTemp.docx");
            File.Copy(originalFileName, tempFileName);
            var document = application.Documents.Open(tempFileName);

            List<Person> boss = GetPersons($"SELECT * FROM person WHERE fullname LIKE '{_forWhoComboBox.Text}%'");
            string tmpText = boss[0].Post + "\n" + boss[0].Rank;
            FixTag($"<!{Tags[0]}>", tmpText, document);
            FixTag($"<!{Tags[1]}>", boss[0].Fullname, document);

            List<Person> person = GetPersons($"SELECT * FROM person WHERE fullname LIKE '{_whoComboBox.Text}%'");
            tmpText = person[0].Post + "\n" + person[0].Rank;
            FixTag($"<!{Tags[2]}>", tmpText, document);
            FixTag($"<!{Tags[3]}>", person[0].Fullname, document);

            FixTag($"<!{Tags[4]}>", days.ToString(), document);
            FixTag($"<!{Tags[5]}>", from, document);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Word Document(.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName.Length > 0)
            {
                document.SaveAs2(saveFileDialog.FileName);
                CreateLog();
            }
            else
            {
                MessageBox.Show("Документ не був створений, спробуйте ще раз");
            }
            try
            {
                document.Close();
                File.Delete(tempFileName);
                application.Quit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Отримує список усіх людей з таблиці
        /// </summary>
        /// <param name="query">SQL запит</param>
        /// <param name="text">Необхідний текст при вибірці</param>
        /// <returns>Повертає список людей</returns>
        private List<Person> GetPersons(string query, string text = null)
        {
            List<Person> people = new List<Person>();
            var connection = new SqlConnection("Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader[0]);
                    string fullname = reader[1].ToString();
                    string sex = reader[2].ToString();
                    DateTime birth = (DateTime)reader[3];
                    int age = DateTime.Now.Year - birth.Year;
                    string rank = reader[4].ToString();
                    string post = reader[5].ToString();
                    string adress = reader[6].ToString();
                    string passport = reader[7].ToString();
                    string idcard = reader[8].ToString();
                    string phone = reader[9].ToString();
                    int? idGroup = null;
                    if (reader[10].ToString() != "")
                    {
                        idGroup = Convert.ToInt32(reader[10]);
                    }

                    int? idStaffDep = null;
                    if (reader[11].ToString() != "")
                    {
                        idStaffDep = Convert.ToInt32(reader[11]);
                    }
                    people.Add(new Person(id, fullname, sex, birth, age, rank, post, adress, passport, idcard, phone, idGroup, idStaffDep));
                }
                connection.Close();
                List<Person> peopleTmp = new List<Person>();
                var q = people.OrderBy(x=> x.Fullname.Substring(0, 1));
                foreach (var person in q)
                {
                    peopleTmp.Add(person);
                }
                return peopleTmp;
            }
        }

        public override void CreateLog()
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fileName = Path.Combine(directory, "Patterns\\Patterns.log");
            List<string> logsTnp = new List<string>();
            List<string> logs = new List<string>();
            using (var filestream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var reader = new StreamReader(filestream))
                {
                    string tmp;
                    
                    while ((tmp = reader.ReadLine()) != null)
                    {
                        logsTnp.Add(tmp);
                    }
                    
                }
            }
            if (logsTnp.Count > 12)
            {
                for (int i = logsTnp.Count - 12; i < logsTnp.Count; i++)
                {
                    logs.Add(logsTnp[i]);
                }
                File.Delete(fileName);
                using (var filestream = new FileStream(fileName, FileMode.Append))
                {
                    using (var writer = new StreamWriter(filestream))
                    {
                        foreach (var log in logs)
                        {
                            writer.WriteLine(log);
                        }
                    
                    }
                }
            }
            using (var filestream = new FileStream(fileName, FileMode.Append))
            {
                using (var writer = new StreamWriter(filestream))
                {
                    writer.WriteLine(IconName);
                }
            }
        }
        #endregion
    }
}
