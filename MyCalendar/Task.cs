using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar
{
    class Task
    {
        public string Task_Name { get; set; }
        public string Task_Description { get; set; }
        public string Task_Day { get; set; }
        public string Task_Time { get; set; }
        public string Task_Color { get; set; }
        public Task()
        {

        }
        public Task(string Task_Name, string Task_Description, string Task_Day, string Task_Time, string Task_Color)
        {
            this.Task_Name = Task_Name;
            this.Task_Description = Task_Description;
            this.Task_Day = Task_Day;
            this.Task_Time = Task_Time;
            this.Task_Color = Task_Color;
        }
    }
}
