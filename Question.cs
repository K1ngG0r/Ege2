using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ege
{
    internal class Question
    {
        public string Issue { get; set; }
        public List<string> Response {  get; set; }
        public string Right { get; set; }


        public Question()
        {
            Response = new List<string>();   
        }
        public static Question Create()
        {
            Question question = new Question();

            Console.Write("Enter question: ");
            question.Issue = Console.ReadLine();

            Console.Write("Enter the number of possible answers: ");
            int num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                Console.Write($"Enter the answer option №{i+1}: ");
                question.Response.Add( Console.ReadLine() );
            }

            Console.Write("Enter the correct option number: ");
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

            Console.Write("Your answer option: ");    
            
            if(Console.ReadLine() == Right)
                return true;

            return false;
        }
    }
}
