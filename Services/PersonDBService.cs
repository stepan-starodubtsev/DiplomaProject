using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.Services
{
    internal class PersonDBService
    {  
        private static SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");
        
        public static Person GetPersonByFullname(String fullname)
        {
            Person person = null;
            String query = $"SELECT * FROM person WHERE fullname LIKE N'{fullname}%'";
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
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
                    int? idGroup = null;
                    if (reader[10].ToString() != "NULL")
                    {
                        idGroup = Convert.ToInt32(reader[10]);
                    }

                    int? idStaffDep = null;
                    if (reader[11].ToString() != "NULL")
                    {
                        idStaffDep = Convert.ToInt32(reader[11]);
                    }
                    string login = reader[12].ToString();
                    string password = reader[13].ToString();
                    person = new Person(id, fullname, sex, birth, age, rank, post, adress, passport, idcard, phone, idGroup, idStaffDep);
                }
                return person;
            }
        }

        private List<Person> GetAllPersons()
        {
            List<Person> people = new List<Person>();
            connection.Open();
            String query = $"SELECT * FROM person";
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
                var q = people.OrderBy(x => x.Fullname.Substring(0, 1));
                foreach (var person in q)
                {
                    peopleTmp.Add(person);
                }
                return peopleTmp;
            }
        }
    }
}
