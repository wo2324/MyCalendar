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
            string password = PasswordBox.Password;
            Login(login, password);
        }

        private void Login(string login, string password)
        {
            if (login.Length != 0 && PasswordBox.Password.Length != 0)
            {
                try
                {
                    int id = ParticipantIdGet(login, password);
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

        int ParticipantIdGet(string login, string password)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
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

                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        return Convert.ToInt32(dataSet.Tables[0].Rows[0]["Participant_Id"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
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
