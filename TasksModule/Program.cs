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

            Console.WriteLine("Insert CalendarUser_Id");
            int calendarUser_Id = Convert.ToInt32(Console.ReadLine());
            int task_Id;
            switch (action)
            {
                case 1:
                    ShowTasks(calendarUser_Id);
                    break;
                case 2:
                    AddTask(calendarUser_Id);
                    break;
                case 3:
                    Console.WriteLine("Insert Task_Id");
                    task_Id = Convert.ToInt32(Console.ReadLine());
                    EditTask(calendarUser_Id, task_Id);
                    break;
                case 4:
                    Console.WriteLine("Insert Task_Id");
                    task_Id = Convert.ToInt32(Console.ReadLine());
                    DeleteTask(calendarUser_Id, task_Id);
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }

        static void ShowTasks(int calendarUser_Id)
        {

        }

        static void AddTask(int calendarUser_Id)
        {

        }

        static void EditTask(int calendarUser_Id, int task_Id)
        {

        }

        static void DeleteTask(int calendarUser_Id, int task_Id)
        {

        }
    }
}
