using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject
{
    public class User
    {
        private int _id;
        private string _login;
        private string _password;
        private string _fullname;

        public User()
        {

        }
        public User(int id,
                    string login,
                    string password,
                    string fullname)
        {
            Id = id;
            Fullname = fullname;
            Login = login;
            Password = password;
        }

        public int Id { get => _id; set => _id = value; }
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
