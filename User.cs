using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Ege
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsAdm { get; set; }

        public Dictionary<string, int> Successes { get; set; } = new Dictionary<string, int>();

        static private string SetLogin(AccayntExistDelegate accayntExistDelegate)
        {
            string login;

            Console.Write("Введите логин: ");
            login = Console.ReadLine();

            while (accayntExistDelegate(login) != -1)
            {
                Console.WriteLine("Логин занят, попробуй другой.");

                Console.Write("Введите логин: ");
                login = Console.ReadLine();
            }

            return login;
        }

        static private string SetPassword()
        {
            string password;

            Console.Write("Введите пароль: ");
            password = Console.ReadLine();

            while (password.Length < 5)
            {
                Console.WriteLine("Пороль слишком короткий(минимум 5).");

                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            }

            return password;
        }

        static public string SetDateOfBirth()
        {
            string dateOfBirth;

            Console.Write("Введите дату рождения(дд.мм.гггг): ");
            dateOfBirth = Console.ReadLine();

            while (!Regex.IsMatch(dateOfBirth, @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$"))
            {
                Console.Write("Некоректный формат \nНадо дд.мм.гггг: ");

                dateOfBirth = Console.ReadLine();
            }

            return dateOfBirth;
        }

        public void AddSuccesses(string theme, int successes)
        {
            Successes.Add(theme, successes);
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

        static public User Load(string path)
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

            var user = JsonSerializer.Deserialize<User>(json);
            if (user.Successes == null)
            {
                user.Successes = new Dictionary<string, int>();
            }
            return user;
        }

        static public User CreateAccaunt(AccayntExistDelegate accayntExistDelegate, bool isAdm)
        {
            User user = new User();

            user.Login = SetLogin(accayntExistDelegate);

            user.Password = SetPassword();

            user.DateOfBirth = SetDateOfBirth();

            user.IsAdm = isAdm;

            return user;
        }

        public override string ToString()
        {
            return $"Логин: {Login}\n" +
                $"Пароль: {Password}\n" +
                $"День рождения: {DateOfBirth}";
        }

        public void PrintSuccesses()
        {
            if(Successes.Count == 0)
            {
                Console.WriteLine("Успехов нет(");
                return;
            }

            foreach (var test in Successes)
            {
                Console.WriteLine($"{test.Key,10} : {test.Value,10}%");
            }
        }

        public void ChangeSettings(AccayntExistDelegate accayntExistDelegate)
        {
            Console.Write("Что поменять хотите?\n" +
                "1)Логин\n" +
                "2)Пароль\n" +
                "3)День рождения\n" +
                "Выбор: ");

            string choise = Console.ReadLine();

            switch (choise)
            {
                case "1":
                    Login = SetLogin(accayntExistDelegate);
                    break;
                case "2":
                    Password = SetPassword();
                    break;
                case "3":
                    DateOfBirth = SetDateOfBirth();
                    break;
                default:
                    Console.WriteLine("Неизвестая команда");
                    break;
            }
        }
    }
}