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
using DiplomaProject.Entities;
using DiplomaProject.Services;

namespace DiplomaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;
        internal User CurrentUser { get => _currentUser; set => _currentUser = value; }

        public MainWindow()
        {
            InitializeComponent();
            if (UserDBService.GetAllUsers().Count == 0)
            {
                Registration registration = new Registration();
                registration.CurrentUser = this.CurrentUser;
                registration.Show();
                this.Close();
            }
        }
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = login_textBox.Text;
            string passwordUser = pass_passwordBox.Password;
            CurrentUser = UserDBService.GetUserByLogin(loginUser);
            if (CurrentUser != null)
            {
                if (CurrentUser.Password == passwordUser)
                {
                    MainMenu mainMenu = new MainMenu(CurrentUser);
                    mainMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильний логін або пароль!");
                    login_textBox.Clear();
                    pass_passwordBox.Clear();
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
}
