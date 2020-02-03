using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PanelWindow : Window
    {
        public Participant Participant { get; }
        public PanelWindow(Participant participant)
        {
            this.Participant = participant;
            InitializeComponent();
            AdjustControls();
        }

        private void AdjustControls()
        {
            AdjustPlannerListBox();    
            AdjustParticipantLabel();
        }

        private void AdjustPlannerListBox()
        {
            PlannersListBox.ItemsSource = GetPlannerName(Utils.DbHandler.GetPlannerName(Participant.Participant_Id));
        }

        private List<string> GetPlannerName(DataSet dataSet)
        {
            List<string> PlannerName = new List<string>();
            if (dataSet.Tables.Count == 0)
            {
                //jak nie ma żadnego plannera to sposób wyświetlania
            }
            else if (dataSet.Tables.Count == 1)
            {
                DataTable dataTable = dataSet.Tables[0];
                foreach (DataRow item in dataTable.Rows)
                {
                    PlannerName.Add(item["Planner_Name"].ToString());
                }
            }
            return PlannerName;
        }

        private void AdjustParticipantLabel()
        {
            ParticipantLabel.Content = this.Participant.Participant_Name;
        }

        private void PlannersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlannerWindow planner = new PlannerWindow(GetContent(PlannersListBox.SelectedItem.ToString()));
            planner.Show();
        }

        private void CreatePlannerButton_Click(object sender, RoutedEventArgs e)//obsługa plannerDescription
        {
            CreatePlanner(CreatePlannerTextBox.Text, null, Participant.Participant_Id); 
            PlannerWindow planner = new PlannerWindow(GetContent(CreatePlannerTextBox.Text));
            planner.Show();
            AdjustPlannerListBox();
            CreatePlannerTextBox.Clear();
        }

        private void CreatePlanner(string plannerName, string plannerDescription, int participantId)
        {
            Utils.DbHandler.CreatePlanner(plannerName, plannerDescription, participantId);
            InitializeTask(plannerName);
        }

        #region toWork
        private void InitializeTask(string plannerName)
        {
            DataTable content = new DataTable(plannerName);
            content.Columns.Add("tvp_Task_Name", typeof(string));
            content.Columns.Add("tvp_Task_Description", typeof(string));
            content.Columns.Add("tvp_Task_Day", typeof(string));
            content.Columns.Add("tvp_Task_Time", typeof(string));
            content.Columns.Add("tvp_Task_Color", typeof(string));

            DayOfWeek dayOfWeek = (DayOfWeek)0;
            string time;
            int hour;
            int minute;

            int startHour = 5;
            int startMinute = 0;

            int timeRange;

            hour = startHour;
            minute = startMinute;
            timeRange = 30;

            while ((int)dayOfWeek < 7)
            {
                while (hour <= 23)
                {
                    time = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
                    content.Rows.Add(null, null, dayOfWeek.ToString(), time, null);
                    if (minute < 60 - timeRange)
                    {
                        minute += timeRange;
                    }
                    else
                    {
                        hour++;
                        minute = 0;
                    }
                }
                hour = startHour;
                minute = startMinute;
                dayOfWeek++;
            }

            //Wysłanie danych do db
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_TaskAdd";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_tvp_Task", content));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private DataTable GetContent(string plannerName)
        {
            return AdjustContent(Utils.DbHandler.GetTask(plannerName, Participant.Participant_Id));
        }



        private DataTable AdjustContent(DataSet dataSet)
        {
            DataTable result = new DataTable("Result");
            result.Columns.Add("Monday", typeof(string));
            result.Columns.Add("Tuesday", typeof(string));
            result.Columns.Add("Wedneday", typeof(string));
            result.Columns.Add("Thursday", typeof(string));
            result.Columns.Add("Friday", typeof(string));
            result.Columns.Add("Saturday", typeof(string));
            result.Columns.Add("Sunday", typeof(string));

            DayOfWeek dayOfWeek = (DayOfWeek)0;
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
                DataTable dataTable = dataSet.Tables[0];
                var results =
                    from myRow in dataTable.AsEnumerable()
                    where myRow.Field<string>("Task_Time") == time
                    select myRow;

                TaskConverter taskToPlanner = new TaskConverter();

                List<Task> taskRestult = new List<Task>();
                foreach (var item in results)
                {

                    if (item["Task_Day"].ToString() == "Monday")
                    {
                        taskToPlanner.MondayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Tuesday")
                    {
                        taskToPlanner.TuesdayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Wednesday")
                    {
                        taskToPlanner.WednesdayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Thursday")
                    {
                        taskToPlanner.ThursdayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Friday")
                    {
                        taskToPlanner.FridayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Saturday")
                    {
                        taskToPlanner.SaturdayTask = item["Task_Name"].ToString();
                    }
                    else if (item["Task_Day"].ToString() == "Sunday")
                    {
                        taskToPlanner.SundayTask = item["Task_Name"].ToString();
                    }
                }

                result.Rows.Add(taskToPlanner.MondayTask, taskToPlanner.TuesdayTask, taskToPlanner.WednesdayTask, taskToPlanner.ThursdayTask
                        , taskToPlanner.FridayTask, taskToPlanner.SaturdayTask, taskToPlanner.SundayTask);

                if (minute < 60 - timeRange)
                {
                    minute += timeRange;
                }
                else
                {
                    hour++;
                    minute = 0;
                }
            }

            return result;
        }
        #endregion


        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            CloseWindows();
        }

        private void CloseWindows()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.Title == "PlannerWindow" || item.Title == "PanelWindow")
                {
                    item.Close();
                }
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(this.Participant);
            accountWindow.ShowDialog();
        }
    }
}
