using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Ege
{
    internal class User
    {
        public string Login {  get; set; }
        public string Password { get; set; }
        public string DateOfBirth {  get; set; }
        public bool IsAdm {  get; set; }

        public Dictionary<string, int> Successes { get; set; } = new Dictionary<string, int>();

        static private string SetLogin(AccayntExistDelegate accayntExistDelegate)
        {
            string login;

            Console.Write("Enter login: ");
            login = Console.ReadLine();

            while (accayntExistDelegate(login) != -1)
            {
                Console.WriteLine("Sorry, the username is busy, try another one.");

                Console.Write("Enter login: ");
                login = Console.ReadLine();
            }

            return login;
        }

        static private string SetPassword()
        {
            string password;

            Console.Write("Enter password: ");
            password = Console.ReadLine();

            while (password.Length < 5)
            {
                Console.WriteLine("Sorry, your password is too short, at least 5 characters.");

                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }

            return password;
        }

        static public string SetDateOfBirth()
        {
            string dateOfBirth;

            Console.Write("Enter your birthday: ");
            dateOfBirth = Console.ReadLine();

            while (!Regex.IsMatch(dateOfBirth, @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$"))
            {
                Console.Write("Incorrect format of date\nTry XX.XX.XXXX: ");

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
            return $"Login: {Login}\n" +
                $"Password: {Password}\n" +
                $"Date of birth: {DateOfBirth}\n";
        }

        public void PrintSuccesses()
        {
            foreach (var test in Successes)
            {
                Console.WriteLine($"{test.Key, 10} : {test.Value, 10}");
            }
        }

        public void ChangeSettings(AccayntExistDelegate accayntExistDelegate)
        {
            Console.Write("What do you want to change?\n" +
                "1)Login\n" +
                "2)Password\n" +
                "3)Date of birth\n" +
                "Choise: ");

            string choise = Console.ReadLine();

            switch(choise)
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
                    Console.WriteLine("Unknown comamnd");
                    break;
            }
        }
    }
}