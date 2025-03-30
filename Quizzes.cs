using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Ege
{
    public class Quizzes
    {
        public List<Quiz> Tests { get; set; }

        public Quizzes()
        {
            Tests = new List<Quiz>();
        }

        public void Save(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine(path);
                throw new Exception();
            }

            foreach (string filePath in Directory.GetFiles(path))
                File.Delete(filePath);


            int i = 0;

            foreach (Quiz quiz in Tests)
            {
                quiz.Save($"{path}quiz({i++}).bin");
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

            foreach (var item in Tests)
            {
                if (item.Theme == name)
                    return i;
                i++;
            }

            return -1;
        }

        public int Test(string name)
        {
            return Tests[IsExist(name)].Passing();
        }

        public override string ToString()
        {
            string name = "Доступные тесты: \n";

            int i = 0;

            foreach (var item in Tests)
            {
                name += $"{i++}) {item.Theme}\n";
            }

            return name;
        }
    }
}