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
        public AccountWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;
            string newPassword;
            if (password.Length != 0 && NewPasswordBox.Password.Length != 0 && NewPasswordBox_1.Password.Length != 0)
            {
                if (true)   //walidacja hasła
                {
                    if (NewPasswordBox.Password == NewPasswordBox_1.Password)
                    {
                        newPassword = NewPasswordBox.Password;
                        EditPassword(1, newPassword);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Given new passwords are non-identical");
                        PasswordBox.Clear();
                        NewPasswordBox.Clear();
                        NewPasswordBox_1.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bad password");
                    PasswordBox.Clear();
                    NewPasswordBox.Clear();
                    NewPasswordBox_1.Clear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBox.Clear();
                NewPasswordBox.Clear();
                NewPasswordBox_1.Clear();
            }
        }
        private void EditPassword(int Participant_Id, string newPassword)
        {
            try
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
                MessageBox.Show("Password has been edit");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                PasswordBox.Clear();
                NewPasswordBox.Clear();
                NewPasswordBox_1.Clear();
            }
        }
    }
}
