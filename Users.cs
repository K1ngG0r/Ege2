using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Ege
{
    public class Users
    {
        public List<User> ListUsers { get; set; }

        public Users()
        {
            ListUsers = new List<User>();
        }

        public int IsExist(string name)
        {
            int i = 0;

            foreach (var item in ListUsers)
            {
                if (item.Login == name)
                    return i;
                i++;
            }

            return -1;
        }

        public int AccauntExist(string login)
        {
            if (ListUsers == null)
                return -1;

            for (int i = 0; i < ListUsers.Count; i++)
            {
                if (ListUsers[i].Login == login)
                {
                    return i;
                }
            }

            return -1;
        }

        public User CreateAccaunt(bool isAdm)
        {
            User user = new User();

            user = User.CreateAccaunt(AccauntExist, isAdm);

            ListUsers.Add(user);

            Save("data/usr/");

            return user;
        }

        public User LogIn(Users users)
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            if (AccauntExist(login) == -1)
            {
                Console.Write("Такого логина нет, попробуй снова!.\n" +
                    "Нажми любую кнопку");

                Console.ReadKey();

                return Menu.Registration(users);
            }

            Console.Write("Введи пороль: ");
            string password = Console.ReadLine();

            int poss = AccauntExist(login);

            while (password != ListUsers[poss].Password)
            {
                Console.Write("Некоректный пороль.\n" +
                    "Введите пороль: ");
                password = Console.ReadLine();
            }

            return ListUsers[poss];
        }

        public void Save(string path)
        {
            if (File.Exists(path))
                throw new ArgumentException();

            foreach (string filePath in Directory.GetFiles(path))
                File.Delete(filePath);

            int i = 0;

            foreach (User user in ListUsers)
            {
                user.Save($"{path}user({i++}).bin");
            }
        }

        public void Load(string path)
        {
            foreach (string files in Directory.GetFiles(path))
            {
                if (Regex.IsMatch(files, "user"))
                {
                    ListUsers.Add(User.Load(files));
                }
            }
        }

        public void PrintUsers()
        {
            int i = 0;

            foreach (var item in ListUsers)
            {
                Console.WriteLine($"{i++}) {item.Login}");
            }
        }
    }
}