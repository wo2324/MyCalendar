using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MyCalendar");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Log in");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    Login();
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }

        static void CreateAccount()
        {
            Console.WriteLine("Insert login and password.");
            string login = Console.ReadLine();
            string password = Console.ReadLine();

            Console.WriteLine("login: {0}\npassword: {1}", login, password);

        }

        static void Login()
        {
            Console.WriteLine("Insert login and password.");
            string login = Console.ReadLine();
            string password = Console.ReadLine();

            Console.WriteLine("login: {0}\npassword: {1}", login, password);
        }
    }
}
