using MyCalendar.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public string plannerName;

        public PlannerWindow(Planner planner)
        {
            InitializeComponent();
            PlannerDataGrid.ItemsSource = planner.Task.DefaultView;
            this.plannerName = planner.PlannerName;
        }

        private void CreatePlannerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataView value = PlannerDataGrid.ItemsSource as DataView;
                DataTable dataTable = value.ToTable();
                DataTable result = new DataTable("Result");
                result.Columns.Add("task", typeof(string));
                result.Columns.Add("day", typeof(string));
                result.Columns.Add("time", typeof(string));

                StartTime = "05:00";
                StopTime = "00:00";
                TimeSpan = "00:30";

                int counter = 0;
                string day;
                string hour;
                //value, day, hour
                int startHour = Int32.Parse(StartTime.Substring(0, 2));
                int startMinute = Int32.Parse(StartTime.Substring(3, 2));
                int stopHour = Int32.Parse(StopTime.Substring(0, 2));
                int stopMinute = Int32.Parse(StopTime.Substring(3, 2));
                int timeSpanHour = Int32.Parse(TimeSpan.Substring(0, 2));
                int timeSpanMinute = Int32.Parse(TimeSpan.Substring(3, 2));

                int actualHour = startHour;
                int actualMinute = startMinute;

                while (actualHour != stopHour)
                {
                    while (actualMinute != 60)
                    {
                        string time = $"{actualHour.ToString("D2")}:{actualMinute.ToString("D2")}";
                        result.Rows.Add(dataTable.Rows[counter]["Monday"], "Monday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Tuesday"], "Tuesday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Wedneday"], "Wednesday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Thursday"], "Thursday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Friday"], "Friday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Saturday"], "Saturday", time);
                        result.Rows.Add(dataTable.Rows[counter]["Sunday"], "Sunday", time);
                        actualMinute += timeSpanMinute;
                        counter++;
                        if (counter == 38)
                        {
                            int s = 1;
                        }
                    }
                    actualMinute = 0;
                    actualHour++;
                    if (actualHour == 24)
                    {
                        actualHour = 0;
                    }
                }

                EditTask(this.plannerName, result);

                GetDataGridRows();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void EditTask(string plannerName, DataTable task)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_TaskEdit";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_tvp_Task", task));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void GetDataGridRows()
        {
            List<string> dataList = new List<string>();

            for (int i = 0; i < PlannerDataGrid.Items.Count; i++)
            {
                for (int j = 0; j < PlannerDataGrid.Columns.Count; j++)
                {
                    //loop throught cell
                    DataGridCell cell = GetCell(i, j);
                    TextBlock tb = cell.Content as TextBlock;
                    if (tb.Text == "sen")
                    {
                        cell.Background = Brushes.Red;
                    }
                }
            }

            //foreach (DataRowView row in PlannerDataGrid.Items)
            //{
            //    for (int i = 0; i < row.Row.ItemArray.Length; i++)
            //    {
            //        if (i != 0)
            //        {
            //            if ((string)row.Row[i] == "Wojciech")
            //            {
            //                DataGridCell firstColumnInFirstRow = row.Row[i] as DataGridCell;
            //                firstColumnInFirstRow.Background = Brushes.Red;
            //            }
            //            dataList.Add((string)row.Row[i]);
            //        }
            //    }
            //}
        }

        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    PlannerDataGrid.ScrollIntoView(rowContainer, PlannerDataGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)PlannerDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                PlannerDataGrid.UpdateLayout();
                PlannerDataGrid.ScrollIntoView(PlannerDataGrid.Items[index]);
                row = (DataGridRow)PlannerDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
