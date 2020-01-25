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

namespace MyCalendar.Test
{
    /// <summary>
    /// Interaction logic for Planner3.xaml
    /// </summary>
    public partial class Planner3 : Window
    {
        ObservableCollection<PlannerElement> Elements = new ObservableCollection<PlannerElement>();

        public Planner3()
        {
            InitializeComponent();

            Elements.Add(new PlannerElement("12:15",
                "meal", "meal", "meal", "meal",
                "meal", "meal", "meal"));
            Elements.Add(new PlannerElement("12:30", 
                "work", "work", "work", "study",
                "study", "study", "chill"));
            Elements.Add(new PlannerElement("13:15",
                "work", "work", "work", "study",
                "study", "study", "chill"));

            PlannerDataGrid.ItemsSource = Elements;
        }
    }
}
