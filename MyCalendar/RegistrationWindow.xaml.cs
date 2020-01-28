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
                        CreateAccount(login, password);
                        MessageBox.Show("Account {0} has been created", login);
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Login(login, password);
                        this.Close();
                    }
                    catch (SqlException sqlException) when (sqlException.Number == 2627)
                    {
                        MessageBox.Show("Account {0} already exists", login);
                        PasswordBoxesClear();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        PasswordBoxesClear();
                    }
                }
                else
                {
                    MessageBox.Show("Given passwords are non-identical");
                    PasswordBoxesClear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordBoxesClear();
            }
        }

        private void CreateAccount(string login, string password)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_ParticipantAdd";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Name", login));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Password", password));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        private void PasswordBoxesClear()
        {
            PasswordBox.Clear();
            PasswordBox_1.Clear();
        }
    }
}
