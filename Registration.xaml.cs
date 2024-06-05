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
using DiplomaProject.Entities;
using DiplomaProject.Services;

namespace DiplomaProject
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private List<Person> _people = new List<Person>();
        private User _currentUser;
        public List<Person> People { get => _people; set => _people = value; }
        public User CurrentUser { get => _currentUser; set => _currentUser = value; }

        public Registration()
        {
            InitializeComponent();
        }
       
        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textBox.Text;
            string password = pass_passwordBox.Password;
            string fullname = fullname_textBox.Text;

            CurrentUser = UserDBService.CreateUser(login, password, fullname);
            if (CurrentUser != null) {
                MainMenu mainMenu = new MainMenu(CurrentUser);
                mainMenu.Show();
                this.Close();
            } 
            else
            {
                MessageBox.Show("Сталася помилка, спробуйте ще раз");
                login_textBox.Clear();
                pass_passwordBox.Clear();
                fullname_textBox.Clear();
            }
        }
    }
}
