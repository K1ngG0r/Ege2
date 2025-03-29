using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ege
{
    internal class Quiz
    {
        public string Theme {  get; set; }
        public List<Question> Test {  get; set; }

        public Quiz() 
        {
            Test = new List<Question>();
        }

        public void Save(string path)
        {
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

            return JsonSerializer.Deserialize<Quiz>(json);
        }

        public static Quiz Create()
        {
            Quiz quiz = new Quiz();

            Console.Write("Enter theme: ");
            quiz.Theme = Console.ReadLine();

            Console.Write("Enter the number of questions: ");
            int num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                quiz.Test.Add(Question.Create());    
            }

            return quiz;
        }

        public int Passing()
        {
            Console.WriteLine($"{Theme, 50}");

            int passing = 0;

            foreach (var item in Test)
            {
                if(item.Answer())
                    passing++;
            }

            Console.WriteLine($"{passing}/{Test.Count}");

            return passing;
        }
    }
}
