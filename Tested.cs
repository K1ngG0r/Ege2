using System;
using System.Xml.Linq;

namespace Ege
{
    public class Tested
    {
        public static string NormalName(User user, Users listUsers, Quizzes quizzes)
        {
            string itemName = Console.ReadLine();
            int poss;

            if (int.TryParse(itemName, out poss))
            {
                if (poss >= quizzes.Tests.Count)
                {
                    Console.WriteLine("Такого теста нет(");
                    Console.ReadKey();

                    Menu.User(user, listUsers, quizzes);
                }

                else
                {
                    itemName = quizzes.Tests[int.Parse(itemName)].Theme;
                }
            }

            while (quizzes.IsExist(itemName) == -1)
            {
                Console.WriteLine("Такого теста нет.");
                itemName = Console.ReadLine();
            }

            return itemName;
        }

        public static void NewTest(User user, Users listUsers, Quizzes quizzes)
        {
            if (quizzes.Tests.Count == 0)
            {
                Console.WriteLine("Тестов нет(");
                return;
            }

            Console.WriteLine(quizzes);

            Console.Write("\nВведите название теста: ");
            string itemName = NormalName(user, listUsers, quizzes);

            int res = quizzes.Test(itemName);

            user.Successes[itemName] = res;


            int poss = quizzes.IsExist(itemName);

            //quizzes.Tests[poss].AddResoult(user, res);
        }

        /*public static Quiz RandomQuiz(Users listUsers)
        {
            Quiz quiz = new Quiz();

            Console.Write("Введите количество вопросов: ");
            int num = int.Parse(Console.ReadLine());

            Random random = new Random();

            for (int i = 0; i < num; i++)
            {
                quiz.
            }

            return quiz;
        }*/
    }
}
