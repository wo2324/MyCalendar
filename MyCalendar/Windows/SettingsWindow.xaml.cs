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
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        public Participant Participant { get; }
        public AccountWindow(Participant participant)
        {
            this.Participant = participant;
            InitializeComponent();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword(PasswordBox.Password, NewPasswordBox.Password, NewPasswordBox_1.Password);
        }

        private void ChangePassword(string password, string newPassword, string newPasswordSample)
        {
            if (password.Length != 0 && newPassword.Length != 0 && newPasswordSample.Length != 0)
            {
                if (this.Participant.Participant_Password == password)
                {
                    if (newPassword == newPasswordSample)
                    {
                        if (password != newPassword)
                        {
                            try
                            {
                                Utils.DbHandler.EditPassword(this.Participant.Participant_Id, newPassword);
                                MessageBox.Show("Password has been edit");
                                this.Close();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                                PasswordBoxClear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The new password must be different from the current one");
                            PasswordBoxClear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Given new passwords are non-identical");
                        PasswordBoxClear();
                    }
                }
                else
                {
                    MessageBox.Show("Bad password");
                    PasswordBoxClear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBoxClear();
            }
        }

        private void PasswordBoxClear()
        {
            PasswordBox.Clear();
            NewPasswordBox.Clear();
            NewPasswordBox_1.Clear();
        }
    }
}
