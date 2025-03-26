﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ege
{
    internal class Users
    {
        public List<User> ListUsers { get; set; }

        public Users() 
        {
            ListUsers = new List<User>();
        }

        private int AccauntExist(string login)
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

        public User CreateAccaunt()
        {
            User user = new User();

            user = User.CreateAccaunt(AccauntExist);

            ListUsers.Add(user);

            Console.WriteLine("You have successfully registered!");

            return user;
        }

        public User LogIn()
        {
            Console.Write("Enter login: ");
            string login  = Console.ReadLine();

            if(AccauntExist(login) == -1)
            {
                Console.WriteLine("There is no such login, try another one.");

                login = Console.ReadLine();
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            int poss = AccauntExist(login);

            while (password != ListUsers[poss].Password)
            {
                Console.WriteLine("Incorrect password, please try again.");
                password = Console.ReadLine();
            }

            Console.WriteLine("You have successfully logged in to your account");

            return ListUsers[poss];
        }

        public void Save(string path)
        {
            if (File.Exists(path))
                throw new ArgumentException();

            int i = 0;

            foreach (User user in ListUsers)
            {
                user.Save($"{path}user({i++}).bin");
            }
        }

        public void Load(string path) 
        {
            foreach(string files in Directory.GetFiles(path))
            {
                if (Regex.IsMatch(files, "user"))
                {
                    ListUsers.Add(User.Load(files));
                }
            }
        }


    }
}