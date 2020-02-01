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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCalendar
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login(LoginTextBox.Text, PasswordBox.Password);
        }

        public void Login(string login, string password)
        {
            if (login.Length != 0 && password.Length != 0)
            {
                try
                {
                    int id = Utils.DbHandler.ParticipantIdGet(login, password);
                    if (id != 0)
                    {
                        PanelWindow mainWindow = new PanelWindow(new Participant(id, login, password));
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bad user name or password");
                        PasswordBox.Clear();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    PasswordBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBox.Clear();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationWindow createAccount = new RegistrationWindow();
            createAccount.Show();
            this.Close();
        }
    }
}
