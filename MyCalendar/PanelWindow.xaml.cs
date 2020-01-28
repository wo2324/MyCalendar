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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PanelWindow : Window
    {
        ObservableCollection<string> Planners = new ObservableCollection<string>();
        public Participant Participant { get; }
        public PanelWindow(Participant participant)
        {
            this.Participant = participant;
            InitializeComponent();
            AdjustControls();
        }

        private void AdjustControls()
        {
            AdjustPlannersListBox();
            AdjustParticipantLabel();
        }

        private void AdjustPlannersListBox()
        {
            Planners.Add("Sample");
            Planners.Add("PlannerName");
            PlannersListBox.ItemsSource = Planners;
        }

        private void AdjustParticipantLabel()
        {
            ParticipantLabel.Content = this.Participant.Participant_Name;
        }

        private void PlannersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(PlannersListBox.SelectedIndex.ToString());
        }

        private void CreatePlannerButton_Click(object sender, RoutedEventArgs e)
        {
            //tworzenie planu
            Planners.Add(CreatePlannerTextBox.Text);
            CreatePlannerTextBox.Clear();

            NewPlanner newPlanner = new NewPlanner();
            newPlanner.Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(this.Participant);
            accountWindow.ShowDialog();
        }
    }
}
