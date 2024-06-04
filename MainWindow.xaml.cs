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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person _currentUser;
        public Person CurrentUser { get => _currentUser; set => _currentUser = value; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = login_textBox.Text;
            string passwordUser = pass_passwordBox.Password;
            string connStr = "Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connStr);
            connection.Open();
            string query =
                $"SELECT * FROM person WHERE login LIKE '{loginUser}' AND password LIKE '{passwordUser}'";
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
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
                            string login = reader[12].ToString();
                            string password = reader[13].ToString();
                            CurrentUser = new Person(id, fullname, sex, birth, age, rank,
                                post, adress, passport, idcard, phone, idGroup, idStaffDep, login, password);
                        }
                        if (loginUser.Equals("admin"))
                        {
                            Registration registration = new Registration();
                            registration.CurrentUser = this.CurrentUser;
                            registration.Show();
                            this.Close();
                        }
                        else
                        {
                            MainMenu mainMenu = new MainMenu(CurrentUser);
                            mainMenu.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильний логін або пароль!");
                        login_textBox.Clear();
                        pass_passwordBox.Clear();
                    }
                }
            }
            connection.Close();
        }
    }
}
