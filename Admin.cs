using System;

namespace Ege
{
    public class AdminPanel
    {
        private static string NormalLogin(User user, Users listUsers, Quizzes quizzes)
        {
            string itemName = Console.ReadLine();
            int poss;

            if (int.TryParse(itemName, out poss))
            {
                if (poss >= listUsers.ListUsers.Count)
                {
                    Console.WriteLine("Вне диапозона(");
                    Console.ReadKey();

                    Menu.User(user, listUsers, quizzes);
                }

                else
                {
                    itemName = listUsers.ListUsers[int.Parse(itemName)].Login;
                }
            }

            while (listUsers.IsExist(itemName) == -1)
            {
                Console.WriteLine("Такого пользователя нет, попробуй ещё раз!");
                itemName = Console.ReadLine();
            }

            return itemName;
        }

        public static void DeleteAccaunt(User user, Users listUsers, Quizzes quizzes)
        {
            if (listUsers.ListUsers.Count == 1)
            {
                Console.WriteLine("Юзеров, кроме тебя нет");
                return;
            }

            listUsers.PrintUsers();

            Console.Write("\nВведите имя юзера: ");
            string itemName = NormalLogin(user, listUsers, quizzes);

            foreach (var item in listUsers.ListUsers)
            {
                if (item.Login == itemName)
                {
                    listUsers.ListUsers.Remove(item);
                    return;
                }
            }

        }

        public static void DeleteTest(User user, Users listUsers, Quizzes quizzes)
        {
            if (quizzes.Tests.Count == 0)
            {
                Console.WriteLine("Тестов нет нет");
                return;
            }

            Console.WriteLine(quizzes);

            Console.Write("\nВведите тест: ");
            string itemName = Tested.NormalName(user, listUsers, quizzes);

            foreach (var item in quizzes.Tests)
            {
                if (item.Theme == itemName)
                {
                    quizzes.Tests.Remove(item);
                    return;
                }
            }
        }
    }
}
