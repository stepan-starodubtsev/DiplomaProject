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
    /// Interaction logic for PersonPage.xaml
    /// </summary>
    public partial class PersonPage : Window
    {
        private Person _person;
        private User _currentUser;
        public PersonPage(int id, User user)
        {
            InitializeComponent();
            CurrentUser = user;
            userName_textBox.Text = CurrentUser.Fullname;
            userLogin_textBox.Text = CurrentUser.Login;
            sex_combo_box.Items.Add("Ч");
            sex_combo_box.Items.Add("Ж"); 
            if (id != -1)
            {
                Person = PersonDBService.GetPersonById(id);
                fullname_header_textBox.Text = Person.Fullname;
                fullname_text_box.Text = Person.Fullname;
                sex_combo_box.SelectedValue = Person.Sex;
                birth_datetime_picker.SelectedDate = Person.Birth;
                rank_text_box.Text = Person.Rank;
                post_text_box.Text = Person.Post;
                address_text_box.Text = Person.Adress;
                passport_text_box.Text = Person.Passport;
                idcode_text_box.Text = Person.Idcard;
                phone_text_box.Text = Person.Phone;
                unit_text_box.Text = Person.Unit;
            }
        }

        public Person Person { get => _person; set => _person = value; }
        public User CurrentUser { get => _currentUser; set => _currentUser = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            person.Fullname = fullname_text_box.Text;
            person.Sex = sex_combo_box.Text;

            try
            {
                person.Birth = (DateTime)birth_datetime_picker.SelectedDate;
            }
            catch (FormatException)
            {
                birth_datetime_picker.Text = "";
                Console.WriteLine("Неправильний формат дати у полі 'З якої дати'!");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Виявлено помилку!\n{exception.Message}");
            }

            person.Rank = rank_text_box.Text;
            person.Post = post_text_box.Text;
            person.Adress = address_text_box.Text;
            person.Passport = passport_text_box.Text;
            person.Idcard = idcode_text_box.Text;
            person.Phone = phone_text_box.Text;
            person.Unit = unit_text_box.Text;

            if (Person == null)
            {
                PersonDBService.CreatePerson(person);
                MessageBox.Show("Інформацію збережено");
            } else
            {
                person.Id = Person.Id;
                PersonDBService.UpdatePerson(person);
                MessageBox.Show("Інформацію оновлено");
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            PersonsPage page = new PersonsPage(CurrentUser);
            page.Show();
            this.Close();
        }

        private void userOut_link_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
