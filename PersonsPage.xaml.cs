using DiplomaProject.Entities;
using DiplomaProject.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PersonsPage.xaml
    /// </summary>
    public partial class PersonsPage : Window
    {
        private User _currentUser;
        public PersonsPage(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            userName_textBox.Text = CurrentUser.Fullname;
            userLogin_textBox.Text = CurrentUser.Login;
            addingPersonsToDataGrid(PersonDBService.GetAllPersons());
        }

        public User CurrentUser { get => _currentUser; set => _currentUser = value; }

        private void addPerson_btn_Click(object sender, RoutedEventArgs e)
        {
            new PersonPage(-1, CurrentUser).Show();
            this.Close();
        }

        private void editPerson_btn_Click(object sender, RoutedEventArgs e)
        {
            if (persons_data_grid.SelectedItem != null)
            {
                Person selectedPerson = persons_data_grid.SelectedItem as Person;
                if (selectedPerson != null)
                {
                    new PersonPage(selectedPerson.Id, CurrentUser).Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Оберіть службовця в таблиці");
            }
        }

        private void removePerson_btn_Click(object sender, RoutedEventArgs e)
        {
            if (persons_data_grid.SelectedItem != null)
            {
                Person selectedPerson = persons_data_grid.SelectedItem as Person;
                if (selectedPerson != null)
                {
                    PersonDBService.DeletePerson(selectedPerson.Id);
                    MessageBox.Show("Службовець видалений");
                }
            }
            else
            {
                MessageBox.Show("Оберіть службовця в таблиці");
            }
        }
        private void userOut_link_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void addingPersonsToDataGrid(List<Person> persons)
        {
            foreach (var person in persons)
            {
                persons_data_grid.Items.Add(person);
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(CurrentUser);
            mainMenu.Show();
            this.Close();
        }
    }
}
