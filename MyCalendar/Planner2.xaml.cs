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
    /// Interaction logic for Planner2.xaml
    /// </summary>
    public partial class Planner2 : Window
    {
        ObservableCollection<Person> Persons = new ObservableCollection<Person>();

        public Planner2()
        {
            InitializeComponent();

            Persons.Add(new Person("Wojciech", "Klanowski", 23));
            Persons.Add(new Person("Michał", "Lasocki", 22));
            Persons.Add(new Person("Anna", "Kielch", 32));

            PersonDataGrid.ItemsSource = Persons;
        }
    }
}