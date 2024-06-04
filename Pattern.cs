using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace DiplomaProject
{
    public abstract  class Pattern : WorkArea
    {
        private string _iconName;
        private string _iconPath;
        private string _patternName;
        private string _sourse;
        private List<string> _tags = new List<string>();
        
        public string IconName { get => _iconName; set => _iconName = value; }
        public string IconPath { get => _iconPath; set => _iconPath = value; }
        public string PatternName { get => _patternName; set => _patternName = value; }
        public string Sourse { get => _sourse; set => _sourse = value; }
        public List<string> Tags { get => _tags; set => _tags = value; }

        protected Pattern():base() { }
        protected Pattern(MainMenu owner):base(owner)
        {
            AreaOwner = owner;
        }
        protected Pattern(MainMenu owner, string iconName, 
            string iconPath, string name, string sourse, params string[] tags)
            :base(owner)
        {
            IconName = iconName;
            IconPath = iconPath;
            PatternName = name;
            Sourse = sourse;
            Tags.AddRange(tags);
        }
        public abstract void CreateLog();
        public abstract void FixTag(string tag, string text, Word.Document document);
        public abstract void SaveDocument();
        public abstract void PrintDocument();
    }
}
