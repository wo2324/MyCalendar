using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar
{
    public class PlannerElement
    {
        public int Monday { get; set; }
        public int Wtorek { get; set; }
        public int Sroda { get; set; }

        public PlannerElement(int Monday, int Wtorek, int Sroda)
        {
            this.Monday = Monday;
            this.Wtorek = Wtorek;
            this.Sroda = Sroda;
        }
    }
}
