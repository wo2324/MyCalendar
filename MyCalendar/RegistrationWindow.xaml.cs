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
            string login = LoginTextBox.Text;
            string password;
            if (login.Length != 0 && PasswordTextBox.Password.Length != 0 && PasswordTextBox_1.Password.Length != 0)
            {
                if (PasswordTextBox.Password == PasswordTextBox_1.Password)
                {
                    password = PasswordTextBox.Password;
                    CreateAccount(login, password);
                    PanelWindow mainWindow = new PanelWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Given passwords are non-identical");
                    PasswordTextBox.Clear();
                    PasswordTextBox_1.Clear();
                }
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordTextBox.Clear();
                PasswordTextBox_1.Clear();
            }
        }

        private void CreateAccount(string login, string password)
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
                        sqlCommand.CommandText = "mc.usp_ParticipantAdd";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Name", login));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Password", password));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Account {0} has been created", login);
            }
            catch (SqlException sqlException) when (sqlException.Number == 2627)
            {
                MessageBox.Show("Account {0} already exists", login);
                PasswordTextBox.Clear();
                PasswordTextBox_1.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                PasswordTextBox.Clear();
                PasswordTextBox_1.Clear();
            }
        }
    }
}
