using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar
{
    public class PlannerElement
    {
        public string Time { get; set; }
        public string MondayActivity { get; set; }
        public string TuesdayActivity { get; set; }
        public string WednesdayActivity { get; set; }
        public string ThursdayActivity { get; set; }
        public string FridayActivity { get; set; }
        public string SaturdayActivity { get; set; }
        public string SundayActivity { get; set; }
        //Do niego wchodzą klasy zawierające pełne informacje o zadaniu
        public PlannerElement(string Time,
            string MondayActivity, string TuesdayActivity, string WednesdayActivity, string ThursdayActivity,
            string FridayActivity, string SaturdayActivity, string SundayActivity)
        {
            this.Time = Time;
            this.MondayActivity = MondayActivity;
            this.TuesdayActivity = TuesdayActivity;
            this.WednesdayActivity = WednesdayActivity;
            this.ThursdayActivity = ThursdayActivity;
            this.FridayActivity = FridayActivity;
            this.SaturdayActivity = SaturdayActivity;
            this.SundayActivity = SundayActivity;
        }
    }
}
