using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProject.Entities
{
    public class Person
    {
        private int _id;
        private string _fullname;
        private string _sex;
        private DateTime _birth;
        private int _age;
        private string _rank;
        private string _post;
        private string _adress;
        private string _passport;
        private string _idcard;
        private string _phone;
        private string _unit;

        public Person() { }
        public Person(int id,
                      string fullname,
                      string sex,
                      DateTime birth,
                      int age,
                      string rank,
                      string post,
                      string adress,
                      string passport,
                      string idcard,
                      string phone,
                      string unit)
        {
            Id = id;
            Fullname = fullname;
            Sex = sex;
            Birth = birth;
            Age = age;
            Rank = rank;
            Post = post;
            Adress = adress;
            Passport = passport;
            Idcard = idcard;
            Phone = phone;
            Unit = unit;
           
        }

        public int Id { get => _id; set => _id = value; }
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string Sex { get => _sex; set => _sex = value; }
        public DateTime Birth { get => _birth; set => _birth = value; }
        public int Age { get => _age; set => _age = value; }
        public string Rank { get => _rank; set => _rank = value; }
        public string Post { get => _post; set => _post = value; }
        public string Adress { get => _adress; set => _adress = value; }
        public string Passport { get => _passport; set => _passport = value; }
        public string Idcard { get => _idcard; set => _idcard = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Unit { get => _unit; set => _unit = value; }
       
    }
}
