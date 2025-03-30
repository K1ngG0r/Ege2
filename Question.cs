using System;
using System.Collections.Generic;


namespace Ege
{
    public class Question
    {
        public string Issue { get; set; }
        public List<string> Response { get; set; }
        public string Right { get; set; }


        public Question()
        {
            Response = new List<string>();
        }
        public static Question Create()
        {
            Question question = new Question();

            Console.Write("Введите вопрос: ");
            question.Issue = Console.ReadLine();

            Console.Write("Введите количество ответов: ");
            int num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                Console.Write($"{i + 1}) ");
                question.Response.Add(Console.ReadLine());
            }

            Console.Write("Введите правильный ответ: ");
            question.Right = Console.ReadLine();

            return question;

        }
        public bool Answer()
        {
            Console.WriteLine(Issue);

            int i = 1;

            foreach (var item in Response)
            {
                Console.WriteLine($"{i++}) {item}");
            }

            Console.Write("Твой вариант ответа: ");

            if (Console.ReadLine() == Right)
                return true;

            return false;
        }

        public override string ToString()
        {
            string n = Issue;

            int i = 1;

            foreach (var item in Response)
            {
                n += $"\n{i++}) {item}";
            }

            n += $"\nПравильный ответ: {Right}";

            return n;
        }
    }
}