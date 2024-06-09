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
            string query = "SELECT * FROM person WHERE person_fullname = @fullname";

            connection.Open();
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@fullname", fullname);
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

        public static Person GetPersonById(int id)
        {
            Person person = null;
            string query = "SELECT * FROM person WHERE person_id = @id";

            connection.Open();
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
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

        public static Person CreatePerson(Person person)
        {

            connection.Open();
            string query = $"INSERT INTO person (person_fullname, person_sex, person_birth, person_rank, person_post, " +
                $"person_adress, person_passport, person_idcard, person_phone, person_unit)" +
                $" VALUES (@fullname, @sex, @birth, @rank, @post, @address, @passport, @idcard, @phone, @unit)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fullname", person.Fullname);
                command.Parameters.AddWithValue("@sex", person.Sex);
                command.Parameters.AddWithValue("@birth", person.Birth);
                command.Parameters.AddWithValue("@rank", person.Rank);
                command.Parameters.AddWithValue("@post", person.Post);
                command.Parameters.AddWithValue("@address", person.Adress);
                command.Parameters.AddWithValue("@passport", person.Passport);
                command.Parameters.AddWithValue("@idcard", person.Idcard);
                command.Parameters.AddWithValue("@phone", person.Phone);
                command.Parameters.AddWithValue("@unit", person.Unit);
                command.ExecuteNonQuery();
                connection.Close();
                return person;
            }
        }

        public static void UpdatePerson(Person person)
        {
            connection.Open();
            string query = "UPDATE person SET " +
                "person_fullname = @fullname, " +
                "person_sex = @sex, " +
                "person_birth = @birth, " +
                "person_rank = @rank, " +
                "person_post = @post, " +
                "person_adress = @address, " +
                "person_passport = @passport, " +
                "person_idcard = @idcard, " +
                "person_phone = @phone, " +
                "person_unit = @unit " +
                "WHERE person_id = @id";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fullname", person.Fullname);
                command.Parameters.AddWithValue("@sex", person.Sex);
                command.Parameters.AddWithValue("@birth", person.Birth);
                command.Parameters.AddWithValue("@rank", person.Rank);
                command.Parameters.AddWithValue("@post", person.Post);
                command.Parameters.AddWithValue("@address", person.Adress);
                command.Parameters.AddWithValue("@passport", person.Passport);
                command.Parameters.AddWithValue("@idcard", person.Idcard);
                command.Parameters.AddWithValue("@phone", person.Phone);
                command.Parameters.AddWithValue("@unit", person.Unit);
                command.Parameters.AddWithValue("@id", person.Id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void DeletePerson(int id)
        {
            string query = "DELETE FROM person WHERE person_id = @id";
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
