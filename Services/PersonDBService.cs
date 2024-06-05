using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DiplomaProject.Entities;
using Microsoft.Office.Interop.Word;

namespace DiplomaProject.Services
{
    internal class PersonDBService
    {  
        private static SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");
        
        public static Person GetPersonByFullname(String fullname)
        {
            Person person = null;
            String query = $"SELECT * FROM person WHERE person_fullname LIKE N'{fullname}%'";
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                    int id = Convert.ToInt32(reader[0]);
                    string sex = reader[2].ToString();
                    DateTime birth = (DateTime)reader[3];
                    int age = DateTime.Now.Year - birth.Year;
                    string rank = reader[4].ToString();
                    string post = reader[5].ToString();
                    string adress = reader[6].ToString();
                    string passport = reader[7].ToString();
                    string idcard = reader[8].ToString();
                    string phone = reader[9].ToString();
                    string unit = reader[10].ToString();
                    person = new Person(id, fullname, sex, birth, age, rank, post, adress, passport, idcard, phone, unit);
                    }
                }
                else
                {
                    MessageBox.Show("Працівника не знайдено");
                }
                connection.Close();
                return person;
            }
        }

        public static List<Person> GetAllPersons()
        {
            List<Person> people = new List<Person>();
            List<Person> peopleTmp = new List<Person>();
            connection.Open();
            String query = $"SELECT * FROM person";
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows) {
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
                        string unit = reader[10].ToString();
                        people.Add(new Person(id, fullname, sex, birth, age, rank, post, adress, passport, idcard, phone, unit));
                    }
                    connection.Close();
                    var q = people.OrderBy(x => x.Fullname.Substring(0, 1));
                    foreach (var person in q)
                    {
                        peopleTmp.Add(person);
                    }
                }                
            }
            connection.Close();
            return peopleTmp;
        }
    }
}
