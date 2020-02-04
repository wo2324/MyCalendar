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
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register(LoginTextBox.Text, PasswordBox.Password, PasswordBox_1.Password);
        }

        private void Register(string login, string password, string passwordSample)
        {
            if (login.Length != 0 && password.Length != 0 && passwordSample.Length != 0)
            {
                if (password == passwordSample)
                {
                    try
                    {
                        Utils.DbHandler.CreateAccount(login, password);
                        MessageBox.Show($"Account {login} has been created");
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Login(login, password);
                        this.Close();
                    }
                    catch (SqlException sqlException) when (sqlException.Number == 2627)
                    {
                        string messageTextBox = $"Account {login} already exists";
                        MessageBox.Show(messageTextBox);
                        PasswordBoxeClear();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        PasswordBoxeClear();
                    }
                }
                else
                {
                    MessageBox.Show("Given passwords are non-identical");
                    PasswordBoxeClear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBoxeClear();
            }
        }

        private void PasswordBoxeClear()
        {
            PasswordBox.Clear();
            PasswordBox_1.Clear();
        }

        private void BackToLoginWIndowButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
