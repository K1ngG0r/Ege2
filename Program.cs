using System;

namespace Ege
{
	public delegate int AccayntExistDelegate(string username);

    public class Program
	{
		static void Main(string[] args)
		{
            try
			{
				Users listUsers = new Users();
				Quizzes quizzes = new Quizzes();

				User user = new User();

				listUsers.Load("data/usr/");
				quizzes.Load("data/test/");


				user = Menu.Registration(listUsers);

				if (user.IsAdm == true)
					Menu.Admin(user, listUsers, quizzes);

				else
					Menu.User(user, listUsers, quizzes);
				
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

        }
	}
}
