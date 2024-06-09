using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace DiplomaProject
{
    public abstract  class Pattern : WorkArea
    {
        private string _fileName;
        private string _iconPath;
        private string _patternName;
        private string _sourse;
        private List<string> _tags = new List<string>();
        
        public string FileName { get => _fileName; set => _fileName = value; }
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
            FileName = iconName;
            IconPath = iconPath;
            PatternName = name;
            Sourse = sourse;
            Tags.AddRange(tags);
        }
        public  void CreateLog()
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fileName = Path.Combine(directory, "Patterns\\Patterns.log");
            List<string> logsTnp = new List<string>();
            List<string> logs = new List<string>();
            using (var filestream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var reader = new StreamReader(filestream))
                {
                    string tmp;

                    while ((tmp = reader.ReadLine()) != null)
                    {
                        logsTnp.Add(tmp);
                    }

                }
            }
            if (logsTnp.Count > 12)
            {
                for (int i = logsTnp.Count - 12; i < logsTnp.Count; i++)
                {
                    logs.Add(logsTnp[i]);
                }
                File.Delete(fileName);
                using (var filestream = new FileStream(fileName, FileMode.Append))
                {
                    using (var writer = new StreamWriter(filestream))
                    {
                        foreach (var log in logs)
                        {
                            writer.WriteLine(log);
                        }

                    }
                }
            }
            using (var filestream = new FileStream(fileName, FileMode.Append))
            {
                using (var writer = new StreamWriter(filestream))
                {
                    writer.WriteLine(FileName);
                }
            }
        }
        /// <summary>
        /// Заміняє тег на потрібну строку
        /// </summary>
        /// <param name="tag">Потрібний тег</param>
        /// <param name="text">Текст, на який замінять тег</param>
        /// <param name="document">об'єкт типу Word.Document</param>
        public void FixTag(string tag, string text, Word.Document document)
        {
            var range = document.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: tag, ReplaceWith: text);
        }

        private string createTempTemplate()
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var originalFileName = Path.Combine(directory, $"Patterns\\PatternsWord\\{FileName}.docx");
            var tempFileName = Path.Combine(directory, $"Patterns\\PatternsWord\\{FileName}Temp.docx");

            File.Copy(originalFileName, tempFileName);
            return tempFileName;
        }

        public abstract void FixDocumentTags(Document document);

        public void PrintDocument()
        {
            var application = new Word.Application();
            application.Visible = false;

            string tempFileName = createTempTemplate();

            var document = application.Documents.Open(tempFileName);

            try
            {
                FixDocumentTags(document);
                document.PrintOut(true, false, Word.WdPrintOutRange.wdPrintAllDocument,
                                                         Item: Word.WdPrintOutItem.wdPrintDocumentContent, Copies: "1", Pages: "",
                                                         PageType: Word.WdPrintOutPages.wdPrintAllPages, PrintToFile: false, Collate: true,
                                                         ManualDuplexPrint: false);
                CreateLog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            document.Close();
            File.Delete(tempFileName);
            application.Quit();
        }

        public void SaveDocument()
        {

            Word.Application application = new Word.Application();
            application.Visible = false;
            string tempFileName = createTempTemplate();

            var document = application.Documents.Open(tempFileName);
            string newFileName = null;

            FixDocumentTags(document);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Word Document(.docx)|*.docx";
            try
            {
                if (saveFileDialog.ShowDialog() == true && saveFileDialog.FileName.Length > 0)
                {
                    newFileName = saveFileDialog.FileName;
                    document.SaveAs2(newFileName);

                    CreateLog();
                }
                else
                {
                    MessageBox.Show("Документ не був створений, спробуйте ще раз");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            document.Close();
            File.Delete(tempFileName);
            application.Quit();
        }
    }
}
