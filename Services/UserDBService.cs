using DiplomaProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiplomaProject.Services
{
    internal class UserDBService
    {
        private static SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Staff;Integrated Security=True;");

        public static User GetUserByLogin(String login)
        {
            User user = null;
            String query = $"SELECT * FROM users WHERE user_login LIKE N'{login}%'";
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read()) { 
                    int id = Convert.ToInt32(reader[0]);
                    string fullname = reader[1].ToString();
                    string password = reader[3].ToString();
                    user = new User(id, login, password, fullname);
                    }
                }
            }
            connection.Close();
            return user;
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            String query = $"SELECT * FROM users";
            connection.Open();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while(reader.Read()){
                        int id = Convert.ToInt32(reader[0]);
                        string fullname = reader[1].ToString();
                        string login = reader[2].ToString();
                        string password = reader[3].ToString();
                        users.Add(new User(id, fullname, login, password));
                    }
                }
            }
            connection.Close();
            return users;
        }

        public static User CreateUser(string login, string password, string fullname)
        {
            if (GetUserByLogin(login) == null)
            {
                connection.Open();
                string query =  $"INSERT INTO users (user_fullname, user_login, user_password) VALUES (@fullname, @login, @password)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fullname", fullname);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return GetUserByLogin(login);
                }
            } 
            return null;
        }
    }
}
