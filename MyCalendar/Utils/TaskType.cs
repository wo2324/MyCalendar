using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar.Windows
{
    public class TaskType
    {
        public string TaskTypeName { get; set; }
        public bool TaskTypeVisibility { get; set; }
        public string TaskTypeDescription { get; set; }
        public string TaskTypeColor { get; set; }

        public TaskType(string TaskTypeName)
        {
            this.TaskTypeName = TaskTypeName;
        }
    }
}
