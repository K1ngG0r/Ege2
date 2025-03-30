using System;
using System.IO;

namespace Ege
{
    public class Menu
    {

        static private void Save(Users listUsers, Quizzes quizzes)
        {
            listUsers.Save("data/usr/");
            quizzes.Save("data/test/");
        }

        static public void Logo()
        {
            string filePath = "tmp/LOGO.txt";

            string LOGO = File.ReadAllText(filePath);

            Console.WriteLine(LOGO);
        }

        #region Reg
        static public void HelloMenu()
        {
            Logo();

            Console.WriteLine("\n0)Выход");
            Console.WriteLine("1)Войти в аккаунт");
            Console.WriteLine("2)Создать аккаунт");
        }

        static public User Registration(Users users)
        {
            Console.Clear();

            HelloMenu();

            string choise = Console.ReadLine();

            switch (choise)
            {
                case "0":
                    Console.WriteLine("Пока(");
                    Environment.Exit(0);
                    break;

                case "1":
                    return users.LogIn(users);

                case "2":
                    return users.CreateAccaunt(false);

                default:
                    Console.WriteLine("Неизвестная комманда, попробуй снова!");

                    Console.WriteLine("Нажми любую кнопку");
                    Console.ReadKey();

                    return Registration(users);

            }

            return null;
        }
        #endregion

        #region Tested
        static public void TestedMenu()
        {
            Logo();

            Console.WriteLine("\n1)Пройти викторину" +
                "\n2)Показать результаты" +
                "\n3)Показать топ 20 по выбранной викторине" +
                "\n4)Рандомная викторина" +
                "\n5)Поменять настройки" +
                "\n6)Удалить результаты" +
                "\n7)Выход из прилоожения" +
                "\n8)Выход из аккаунта");
        }

        static public void TestedAction(string choise, User user, Users listUsers, Quizzes quizzes)
        {
            switch (choise)
            {
                case "1":
                    Tested.NewTest(user, listUsers, quizzes);

                    Save(listUsers, quizzes);
                    break;

                case "2":
                    user.PrintSuccesses();

                    Console.WriteLine("Нажми любую кнопку");
                    Console.ReadKey();

                    break;

                case "3":

                    break;

                case "4":
                    Console.WriteLine(user);

                    user.ChangeSettings(listUsers.AccauntExist);
                    
                    Save(listUsers, quizzes);
                    
                    Console.WriteLine("Нажми любую кнопку");
                    Console.ReadKey();
                    break;

                case "5":
                    user.Successes.Clear();
                    Save(listUsers, quizzes);
                    break;

                case "6":
                    Console.WriteLine("Пока(");
                    Environment.Exit(0);
                    break;

                case "7":
                    user = Registration(listUsers);

                    Save(listUsers, quizzes);

                    if (user.IsAdm == true)
                        Admin(user, listUsers, quizzes);

                    else
                        User(user, listUsers, quizzes);

                    break;

                default:
                    Console.WriteLine("Неизвестная комманда, попробуй снова!");

                    Console.WriteLine("Нажми любую кнопку");
                    Console.ReadKey();
                    break;
            }
        }

        static public void User(User user, Users listUsers, Quizzes quizzes)
        {
            Console.Clear();

            TestedMenu();

            string choise = Console.ReadLine();

            TestedAction(choise, user, listUsers, quizzes);

            User(user, listUsers, quizzes);
        }
        #endregion

        #region Admin
        static public void AdminMenu()
        {
            Logo();

            Console.WriteLine("1)Создать викторину" +
                "\n2)Создать админ аккаунт" +
                "\n3)Удалить аккаунт" +
                "\n4)Удалить тест" +
                "\n5)Выйти из приложения" +
                "\n6)Выйти из аккаунта");
        }

        static public void AdminAction(string choise, User user, Users listUsers, Quizzes quizzes)
        {
            switch (choise)
            {
                case "1":
                    quizzes.Add(Quiz.Create());
                    break;

                case "2":
                    listUsers.CreateAccaunt(true);
                    break;

                case "3":
                    AdminPanel.DeleteAccaunt(user, listUsers, quizzes);
                    break;

                case "4":
                    AdminPanel.DeleteTest(user, listUsers, quizzes);
                    break;

                case "5":
                    Console.WriteLine("Bye(");
                    Environment.Exit(0);
                    break;

                case "6":
                    user = Registration(listUsers);

                    if (user.IsAdm == true)
                        Admin(user, listUsers, quizzes);

                    else
                        User(user, listUsers, quizzes);
                    break;

                default:
                    Console.WriteLine("Неизвестная комманда, попробуй снова!");
                    break;
            }

            Console.WriteLine("Нажми любую кнопку");
            Console.ReadKey();

            Save(listUsers, quizzes);
        }

        static public void Admin(User user, Users listUsers, Quizzes quizzes)
        {
            Console.Clear();

            AdminMenu();
            string choise = Console.ReadLine();

            AdminAction(choise, user, listUsers, quizzes);

            Admin(user, listUsers, quizzes);
        }
        #endregion
    }
}
