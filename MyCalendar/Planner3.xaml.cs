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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCalendar.Test
{
    /// <summary>
    /// Interaction logic for Planner3.xaml
    /// </summary>
    public partial class Planner3 : Window
    {
        //ObservableCollection<PlannerElement> Elements = new ObservableCollection<PlannerElement>();
        ObservableCollection<string> Colors = new ObservableCollection<string>();

        public Planner3()
        {
            InitializeComponent();

            //Elements.Add(new PlannerElement("12:15",
            //    "meal", "meal", "meal", "meal",
            //    "meal", "meal", "meal"));
            //Elements.Add(new PlannerElement("12:30", 
            //    "work", "work", "work", "study",
            //    "study", "study", "chill"));
            //Elements.Add(new PlannerElement("13:15",
            //    "work", "work", "work", "study",
            //    "study", "study", "chill"));
            Colors.Add("pink");
            Colors.Add("orange");
            Colors.Add("green");
            Colors.Add("black1");

            ColorList.ItemsSource = Colors;
            FillDataGrid();
        }

        private void ColorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorList.SelectedIndex.ToString();
        }

        private void FillDataGrid()
        {
            DataSet dataSet = new DataSet();

            try
            {
                string connectionString = "Server=localhost;Database=MyCalendar;Trusted_Connection=True";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "mc.usp_TasksGet";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Participant_Id", 1));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlConnection.State == ConnectionState.Closed)
                        {
                            sqlConnection.Open();
                        }

                        sqlDataAdapter.Fill(dataSet);

                        if (dataSet != null)
                        {
                            foreach (DataTable dataTable in dataSet.Tables)
                            {
                                foreach (DataRow dataRow in dataTable.Rows)
                                {
                                    string task_Id = dataRow["Task_Id"].ToString();
                                    string task_Name = dataRow["Task_Name"].ToString();
                                    string task_Description = dataRow["Task_Description"].ToString();

                                    Console.WriteLine("{0} - {1} - {2}", task_Id, task_Name, task_Description);
                                    PlannerDataGrid.ItemsSource = dataTable.DefaultView;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void PlannerDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //GetDataGridRows();
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
                    if (tb.Text == "Task")
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ((DataView)PlannerDataGrid.ItemsSource).ToTable();
            foreach (DataRow dataRow in dt.Rows)
            {
                string id = dataRow["Task_Id"].ToString();
                int s;
            }
        }
    }
}
