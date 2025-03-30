using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;


namespace Ege
{
    public class Quiz
    {
        public string Theme { get; set; }
        public List<Question> Test { get; set; }
        //public Dictionary<User, int> Top { get; set; }


        public Quiz()
        {
            Test = new List<Question>();
            //Top = new Dictionary<User, int>();
        }

        public void Save(string path)
        {
            /*if(Top == null)
                Top = new Dictionary<User, int>();*/

            string json = JsonSerializer.Serialize(this);
            string newJson = "";

            for (int i = 0; i < json.Length; i++)
            {
                newJson += (char)((int)json[i] + 10);
            }


            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                binaryFormatter.Serialize(stream, newJson);
            }
        }

        static public Quiz Load(string path)
        {
            string oldJson;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                oldJson = (string)binaryFormatter.Deserialize(stream);
            }

            string json = "";

            for (int i = 0; i < oldJson.Length; i++)
            {
                json += (char)((int)oldJson[i] - 10);
            }

            Quiz result = JsonSerializer.Deserialize<Quiz>(json);

            /*if (result.Top == null)
            {
                result.Top = new Dictionary<User, int>();
            }*/

            return result;
        }

        public static Quiz Create()
        {
            Quiz quiz = new Quiz();

            Console.Write("Введите название: ");
            quiz.Theme = Console.ReadLine();

            Console.Write("Введите количество вопросов: ");
            int num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                quiz.Test.Add(Question.Create());
            }

            return quiz;
        }

        public int Passing()
        {
            Console.WriteLine($"{Theme,50}");

            int passing = 0;

            foreach (var item in Test)
            {
                if (item.Answer())
                    passing++;
            }

            Console.WriteLine($"{passing}/{Test.Count}");

            return (int)(passing / Test.Count * 100);
        }

        /* void AddResoult(User user, int result)
        {
            Top[user] = result;

            if (Top.Count == 21)
            {
                Top.Remove(Top.Keys.Last());
            }
        }*/

        public override string ToString()
        {
            string n = $"theme: {Theme}";

            foreach (var item in Test)
            {
                n += item.ToString();
            }

            return n;
        }
    }
}