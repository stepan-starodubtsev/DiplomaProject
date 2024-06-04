
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace DiplomaProject
{
    class Content
    {
        private string _text;
        private List<string> _comboText;
        public string Text
        {
            get { return _text; }
            set
            {
                OnTextChanged(value);
            }
        }

        public List<string> ComboText { get => _comboText; set => _comboText = value; }

        private void OnTextChanged(string text)
        {
            string connString = @"Data Source=KOBA-PC\SQLEXPRESS;Initial Catalog=Staff;Integrated Security=True";
            using (var connection = new SqlConnection(connString))
            {
                string query = $"SELECT fullname_pers FROM persons_db WHERE fullname LIKE N'{Text}%'";
                var command = new SqlCommand(query,connection);
                using (var reader = command.ExecuteReader())
                {
                    ComboText = new List<string>();
                    while (reader.Read())
                    {
                        ComboText.Add(reader[0].ToString());    
                    }
                }
            }
        }
    }
}
