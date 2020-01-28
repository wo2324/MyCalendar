using System;
using System.Collections.Generic;
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
            AdjustPlannersListBox();
            AdjustParticipantLabel();
        }

        private void AdjustPlannersListBox()
        {

        }

        private void AdjustParticipantLabel()
        {
            ParticipantLabel.Content = this.Participant.Participant_Name;
        }

        private void PlannersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(PlannersListBox.SelectedIndex.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow(this.Participant);
            accountWindow.Show();
        }
    }
}
