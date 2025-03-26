using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ege
{
    internal class Quizzes
    {
        public List<Quiz> Tests { get; set; }

        public Quizzes()
        {
            Tests = new List<Quiz>();
        }

        public void Save(string path)
        {
            if (File.Exists(path))
                throw new ArgumentException();

            int i = 0;

            foreach (Quiz user in Tests)
            {
                user.Save($"{path}user({i++}).bin");
            }
        }

        public void Load(string path)
        {
            foreach (string files in Directory.GetFiles(path))
            {
                if (Regex.IsMatch(files, "user"))
                {
                    Tests.Add(Quiz.Load(files));
                }
            }
        }

        public void Add(Quiz quiz)
        {
            Tests.Add(quiz);
        }
    }
}