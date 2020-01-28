using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Planner.xaml
    /// </summary>
    public partial class PlannerTest : Window
    {
        ObservableCollection<string> Colors = new ObservableCollection<string>();
        ObservableCollection<PlannerElement> ColorsGrid = new ObservableCollection<PlannerElement>();
        public PlannerTest()
        {
            InitializeComponent();

            Colors.Add("pink");
            Colors.Add("orange");
            Colors.Add("green");
            Colors.Add("black1");

            ColorList.ItemsSource = Colors;

            //ColorsGrid.Add(new PlannerElement(1, 21, 2));
            //ColorsGrid.Add(new PlannerElement(234, 34253, 0));
            //ColorsGrid.Add(new PlannerElement(153, 1, 4536));
            //ColorsGrid.Add(new PlannerElement(0,2351, 124));

            PlannerDataGrid.ItemsSource = ColorsGrid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Colors.Add(ColorToAdd.Text);
            //ColorList.Items.Add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int selectedColorListIndex = ColorList.SelectedIndex;
            if (selectedColorListIndex != -1)
            {
                Colors.RemoveAt(selectedColorListIndex);
            }
        }

        private void ColorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorList.SelectedIndex.ToString();
        }
    }
}
