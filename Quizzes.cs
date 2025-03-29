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
                if (Regex.IsMatch(files, "quiz"))
                {
                    Tests.Add(Quiz.Load(files));
                }
            }
        }

        public void Add(Quiz quiz)
        {
            Tests.Add(quiz);
        }

        public int IsExist(string name)
        {
            int i = 0;

            foreach(var item in Tests)
            {
                if(item.Theme == name) 
                    return i;
                i++;
            }

            return -1;
        }

        public int Test(string name)
        {
            int poss = IsExist(name);

            while (poss == -1)
            {
                Console.WriteLine("There is no such quiz, try again.");
                poss = IsExist(Console.ReadLine());
            }

            return Tests[poss].Passing();
        }

        public override string ToString()
        {
            string name = "Available topics: \n";

            int i = 0;

            foreach (var item in Tests)
            {
                name += $"{i++}) {item.Theme}";
            }

            return name;
        }


    }
}