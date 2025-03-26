using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ege
{
    public delegate int AccayntExistDelegate(string username);

    internal class Program
    {
        static public void Logo()
        {
            string filePath = "tmp/LOGO.txt";

            string LOGO = File.ReadAllText(filePath);

            Console.WriteLine(LOGO);
        }

        static public void HelloMenu()
        {
            Logo();
            
            Console.WriteLine("\n0)Exit");
            Console.WriteLine("1)Log in");
            Console.WriteLine("2)Create account");
        }

        static public User Registration(Users users)
        {
            Console.Clear();

            HelloMenu();

            string choise = Console.ReadLine();

            switch (choise)
            {
                case "0":
                    Console.WriteLine("Bye(");
                    Environment.Exit(0);
                    break;

                case "1":
                    return users.LogIn();

                case "2":
                    return users.CreateAccaunt();

                default:
                    Console.WriteLine("Unknown command, try again");
                    Thread.Sleep(750);
                    Registration(users);

                    break;
            }

            return null;
        }

        static void Main(string[] args)
        {
            try
            {
                Users listUsers = new Users();
                Quizzes quizzes = new Quizzes();

                User user = new User();

                listUsers.Load("data/usr/");

                //user = Registration(listUsers);

                Quiz quiz = Quiz.Load("data/test/quiz.bin");

                quizzes.Add(quiz);

                quiz.Passing();

                listUsers.Save("data/usr/");

            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
