using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TasksModule");
            Console.WriteLine("1. Show tasks");
            Console.WriteLine("2. Add task");
            Console.WriteLine("3. Edit task");
            Console.WriteLine("4. Delete task");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }
    }
}
