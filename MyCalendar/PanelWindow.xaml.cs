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
            AdjustPlannersListBox();    //jak nie ma żadnego plannera to sposób wyświetlania
            AdjustParticipantLabel();
        }

        private void AdjustPlannersListBox()
        {
            PlannersListBox.ItemsSource = GetPlannersNames();
        }

        private List<string> GetPlannersNames()
        {
            return GetPlannersNames(GetPlannersNames(Participant.Participant_Id));
        }

        private DataSet GetPlannersNames(int participantId)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_Planner_NameGet";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Id", participantId));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }

        private List<string> GetPlannersNames(DataSet dataSet)
        {
            List<string> PlannersNames = new List<string>();
            for (int i = 0; i < dataSet.Tables.Count; i++)
            {
                DataTable dataTable = dataSet.Tables[i];
                foreach (DataRow item in dataTable.Rows)
                {
                    PlannersNames.Add(item["Planner_Name"].ToString());
                }
            }
            return PlannersNames;
        }

        private void AdjustParticipantLabel()
        {
            ParticipantLabel.Content = this.Participant.Participant_Name;
        }

        private void PlannersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string plannerName = PlannersListBox.SelectedItem.ToString();
            Planner planner = new Planner(GetContent(plannerName));
            planner.Show();
        }

        private void CreatePlannerButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePlanner(); //obsługa plannerDescription
            Planner planner = new Planner(GetContent(CreatePlannerTextBox.Text));
            planner.Show();
            AdjustPlannersListBox();
            CreatePlannerTextBox.Clear();
        }

        private void CreatePlanner()
        {
            CreatePlanner(CreatePlannerTextBox.Text, null, Participant.Participant_Id);
            InitializeContent(CreatePlannerTextBox.Text);
        }

        private void CreatePlanner(string plannerName, string plannerDescription, int participantId)    //walidacja
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_PlannerAdd";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Description", plannerDescription));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Participant_Id", participantId));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void InitializeContent(string plannerName)
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
            return AdjustContent(GetContent(plannerName, Participant.Participant_Id));
        }

        private DataSet GetContent(string plannerName, int participantId)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_TaskGet";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Participant_Id", participantId));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }

        private DataTable AdjustContent(DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables[0];
            return dataTable;
        }

        //DataTable CreateContent(string planner)
        //{
        //    DataTable content = new DataTable(planner);
        //    content.Columns.Add("Monday", typeof(string));
        //    content.Columns.Add("Tuesday", typeof(string));
        //    content.Columns.Add("Wedneday", typeof(string));
        //    content.Columns.Add("Thursday", typeof(string));
        //    content.Columns.Add("Friday", typeof(string));
        //    content.Columns.Add("Saturday", typeof(string));
        //    content.Columns.Add("Sunday", typeof(string));

        //    #region Time counting
        //    string time;
        //    int hour;
        //    int minute;

        //    int startHour = 5;
        //    int startMinute = 0;

        //    int timeRange;

        //    hour = startHour;
        //    minute = startMinute;
        //    timeRange = 30;
        //    while (hour <= 23)
        //    {
        //        time = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
        //        if (minute < 60 - timeRange)
        //        {
        //            minute += timeRange;
        //        }
        //        else
        //        {
        //            hour++;
        //            minute = 0;
        //        }

        //        content.Rows.Add(null, null, null, null, null, null, null);
        //    }
        //    #endregion
        //    return content;
        //}

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
                if (item.Title == "Planner" || item.Title == "PanelWindow")
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
