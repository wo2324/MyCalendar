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
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;
            if (login.Length != 0 && PasswordTextBox.Password.Length != 0)
            {
                Login(login, password);
            }
            else
            {
                MessageBox.Show("All fields must be filled");
                PasswordTextBox.Clear();
            }
        }

        void Login(string login, string password)
        {
            DataSet dataSet = new DataSet();
            try
            {
                string connectionString = "Server=localhost;Database=MyCalendar;Trusted_Connection=True";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.CommandText = "mc.usp_ParticipantGet";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Name", login));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Password", password));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlConnection.State == ConnectionState.Closed)
                        {
                            sqlConnection.Open();
                        }

                        sqlDataAdapter.Fill(dataSet);

                        if (dataSet.Tables[0].Rows.Count != 0)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Bad user name or password");
                            PasswordTextBox.Clear();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                PasswordTextBox.Clear();
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
