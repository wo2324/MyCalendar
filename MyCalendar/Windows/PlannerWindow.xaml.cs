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
    public partial class PlannerWindow : Window
    {
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string TimeSpan { get; set; }

        public PlannerWindow(DataTable content)
        {
            InitializeComponent();
            PlannerDataGrid.ItemsSource = content.DefaultView;
        }

        private void CreatePlannerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView value = PlannerDataGrid.ItemsSource as DataView;
                DataTable dataTable = value.ToTable();

                StartTime = "05:00";
                StartTime = "00:00";

                string day;
                string hour;
                //value, day, hour
                int startHour = Int32.Parse(StartTime.Substring(2));
                int timeSpan = Int32.Parse(TimeSpan);
                int startMinute = Int32.Parse(StartTime.Substring(3, 5));
                int stopHour = Int32.Parse(StopTime.Substring(2));
                int stopMinute = Int32.Parse(StopTime.Substring(2, 2));

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
