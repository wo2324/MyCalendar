using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        }

        private void FillDataGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "SampleTable";

            DataColumn id = new DataColumn("id", typeof(int));
            DataColumn name = new DataColumn("name", typeof(string));
            DataColumn surname = new DataColumn("surname", typeof(string));
            DataColumn age = new DataColumn("age", typeof(int));

            dataTable.Columns.Add(id);
            dataTable.Columns.Add(name);
            dataTable.Columns.Add(surname);
            dataTable.Columns.Add(age);

            dataTable.Rows.Add(1, "Wojciech", "Klanowski", 23);
            dataTable.Rows.Add(2, "Michał", "Adam", 24);
            dataTable.Rows.Add(3, "Kinga", "Wojciech", 23);
            dataTable.Rows.Add(4, "Wojciech", "Giza", 21);

            PlannerDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void PlannerDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid(); 
            GetDataGridRows();
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
                    if (tb.Text == "Wojciech")
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
