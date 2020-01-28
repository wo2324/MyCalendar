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
                                EditPassword(this.Participant.Participant_Id, newPassword);
                                MessageBox.Show("Password has been edit");
                                this.Close();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                                PasswordBoxesClear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The new password must be different from the current one");
                            PasswordBoxesClear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Given new passwords are non-identical");
                        PasswordBoxesClear();
                    }
                }
                else
                {
                    MessageBox.Show("Bad password");
                    PasswordBoxesClear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBoxesClear();
            }
        }

        private void EditPassword(int Participant_Id, string newPassword)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_PasswordEdit";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Id", Participant_Id));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_NewPassword", newPassword));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void PasswordBoxesClear()
        {
            PasswordBox.Clear();
            NewPasswordBox.Clear();
            NewPasswordBox_1.Clear();
        }
    }
}
