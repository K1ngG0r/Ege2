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
                    return users.CreateAccaunt(false);

                default:
                    Console.WriteLine("Unknown command, try again");
                    Thread.Sleep(750);
                    Registration(users);

                    break;
            }

            return null;
        }

        static public void MainMenu()
        {
            Logo();

            Console.WriteLine("\n1)Start new quiz" +
                "\n2)Show results" +
                "\n3)Show the Top 20 for a specific quiz" +
                "\n4)Change settings" +
                "\n5)Exit");
        }

        static public void AdminMenu()
        {
            Logo();

            Console.WriteLine("\n1)Start new quiz" +
                "\n2)Show results" +
                "\n3)Show the Top 20 for a specific quiz" +
                "\n4)Change settings" +
                "\n5)Create quiz" +
                "\n6)Crate admin accaunt" +
                "\n7)Delete accaunt" +
                "\n7)Exit");
        }

        static void Main(string[] args)
        {
            try
            {
                Users listUsers = new Users();
                Quizzes quizzes = new Quizzes();

                User user = new User(); 

                listUsers.Load("data/usr/");
                quizzes.Load("data/test/");

                user = Registration(listUsers);

                while (true)
                {
                    Console.Clear();
                    MainMenu();

                    string choise = Console.ReadLine();

                    switch(choise)
                    {
                        case "1":
                            Console.WriteLine(quizzes);

                            Console.Write("\nEnter item name: ");
                            string itemName = Console.ReadLine();

                            int res = quizzes.Test(itemName);

                            user.AddSuccesses(itemName, res);
                            break;

                        case "2":
                            user.PrintSuccesses();
                            Console.WriteLine("Press any button");
                            Console.ReadKey();

                            break;

                        case "3":

                            break;

                        case "4":
                            Console.WriteLine(user);

                            user.ChangeSettings(listUsers.AccauntExist);

                            Console.WriteLine("Press any button");  
                            Console.ReadKey();
                            break;

                        case "5":
                            Console.WriteLine("Bye(");
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Unknown command(");
                            Thread.Sleep(1000);
                            break;
                    }

                    listUsers.Save("data/usr/");
                    quizzes.Save("data/test/");
                }

            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
