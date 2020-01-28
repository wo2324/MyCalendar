using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCalendar
{
    /// <summary>
    /// Interaction logic for NewPlanner.xaml
    /// </summary>
    public partial class Planner : Window
    {
        public Planner()
        {
            InitializeComponent();
            CreateNewWindow();
        }

        private void CreateNewWindow()
        {
            DataTable dataTable = new DataTable("NewWindow");
            dataTable.Columns.Add("Time", typeof(string));
            dataTable.Columns.Add("Monday", typeof(string));
            dataTable.Columns.Add("Tuesday", typeof(string));
            dataTable.Columns.Add("Wedneday", typeof(string));
            dataTable.Columns.Add("Thursday", typeof(string));
            dataTable.Columns.Add("Friday", typeof(string));
            dataTable.Columns.Add("Saturday", typeof(string));
            dataTable.Columns.Add("Sunday", typeof(string));

            string time;
            int hour;
            int minute;

            int startHour = 5;
            int startMinute = 0;

            int timeRange;

            hour = startHour;
            minute = startMinute;
            timeRange = 30;
            while (hour <= 23)
            {
                time = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
                if (minute < 60 - timeRange)
                {
                    minute += timeRange;
                }
                else
                {
                    hour++;
                    minute = 0;
                }

                dataTable.Rows.Add(time, "test", null, null, null, null, null, null);
            }

            PlannerDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
