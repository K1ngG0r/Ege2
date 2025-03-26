using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ege
{


    internal class User
    {
        public string Login {  get; set; }
        public string Password { get; set; }
        public string DateOfBirth {  get; set; }
        public Dictionary<Quiz, int> Successes {  get; set; }

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

            return JsonSerializer.Deserialize<User>(json);
        }

        static public User CreateAccaunt(AccayntExistDelegate accayntExistDelegate)
        {
            User user = new User();

            Console.Write("Enter login: ");
            user.Login = Console.ReadLine();

            while (accayntExistDelegate(user.Login) != -1)
            {
                Console.WriteLine("Sorry, the username is busy, try another one.");

                Console.Write("Enter login: ");
                user.Login = Console.ReadLine();
            }

            Console.Write("Enter password: ");
            user.Password = Console.ReadLine();

            while (user.Password.Length < 5)
            {
                Console.WriteLine("Sorry, your password is too short, at least 5 characters.");

                Console.Write("Enter password: ");
                user.Password = Console.ReadLine();
            }

            Console.Write("Enter your birthday: ");
            user.DateOfBirth = Console.ReadLine();

            while (!Regex.IsMatch(user.DateOfBirth, @"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$"))
            {
                Console.Write("Incorrect format of date\nTry XX.XX.XXXX: ");

                user.DateOfBirth = Console.ReadLine();
            }

            return user;
        }

        public override string ToString()
        {
            return $"Login: {Login}\n" +
                $"Password: {Password}\n" +
                $"Date of birth: {DateOfBirth}";
        }
    }
}