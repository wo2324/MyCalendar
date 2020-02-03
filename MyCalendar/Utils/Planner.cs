using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar.Windows
{
    public class Planner
    {
        public string PlannerName { get; set; }
        public DataTable Task { get; set; }

        public Planner(string PlannerName, DataTable Task)
        {
            this.PlannerName = PlannerName;
            this.Task = Task;
        }
    }
}
