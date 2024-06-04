using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

namespace DiplomaProject
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private List<Person> _people = new List<Person>();
        private Person _currentUser = new Person();
        public List<Person> People { get => _people; set => _people = value; }
        public Person CurrentUser { get => _currentUser; set => _currentUser = value; }

        public Registration()
        {
            InitializeComponent();
            GetPeople();
            person_comboBox.ItemsSource = GetPeopleNames();
        }

        /// <summary>
        /// При зміні тексту виводить результати пошуку по коллекції People
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void person_comboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                person_comboBox.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && person_comboBox.SelectedItem == null)
            {
                person_comboBox.IsDropDownOpen = false;
            }

            person_comboBox.IsDropDownOpen = true;
            if (person_comboBox.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(person_comboBox.ItemsSource);
                cv.Filter = s => ((string)s).IndexOf(person_comboBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }
        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textBox.Text;
            string password = pass_passwordBox.Password;
            string fullname = person_comboBox.Text;
            SqlConnection connection = new SqlConnection(
                "Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");
            connection.Open();
            string query =
                $"UPDATE persons SET login = @1, password = @2 WHERE fullname = @3";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@1", login);
            command.Parameters.AddWithValue("@2", password);
            command.Parameters.AddWithValue("@3", fullname);
            command.ExecuteNonQuery();
            connection.Close();
            MainMenu mainMenu = new MainMenu(CurrentUser);
            mainMenu.Show();
            this.Close();
        }
        /// <summary>
        /// Перенаправляє на вікно головного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(CurrentUser);
            mainMenu.Show();
            this.Close();
        }
        #region MainMetods
        /// <summary>
        /// Вибирає в список People всі поля таблиці persons_db
        /// </summary>
        private void GetPeople()
        {
            SqlConnection connection = new SqlConnection(
                "Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");
            connection.Open();
            string query = "SELECT * FROM person";
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader[0]);
                            string fullname = reader[1].ToString();
                            string sex = reader[2].ToString();
                            DateTime birth = (DateTime)reader[3];
                            int age = Convert.ToInt32(reader[4]);
                            string rank = reader[5].ToString();
                            string post = reader[6].ToString();
                            string adress = reader[7].ToString();
                            string passport = reader[8].ToString();
                            string idcard = reader[9].ToString();
                            string phone = reader[10].ToString();
                            int? idGroup = null;
                            if (reader[11].ToString() != "")
                            {
                                idGroup = Convert.ToInt32(reader[11]);
                            }

                            int? idStaffDep = null;
                            if (reader[12].ToString() != "")
                            {
                                idStaffDep = Convert.ToInt32(reader[12]);
                            }
                            People.Add(new Person(id, fullname, sex, birth, age, rank,
                                post, adress, passport, idcard, phone, idGroup, idStaffDep));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            connection.Close();
        }
        /// <summary>
        /// Вибирає повні імена людей з списка People
        /// </summary>
        /// <returns>Список повних імен</returns>
        private List<string> GetPeopleNames()
        {
            List<string> names = new List<string>();
            foreach (var person in People)
            {
                names.Add(person.Fullname);
            }
            return names;
        }
        #endregion
    }
}
