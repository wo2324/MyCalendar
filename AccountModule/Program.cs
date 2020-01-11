using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AccountModule");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Log in");
            int action = Convert.ToInt32(Console.ReadLine());
            switch (action)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    Login();
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }
        static void CreateAccount()
        {
            Console.WriteLine("Insert login and password.");
            string login = Console.ReadLine();
            string password = Console.ReadLine();

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
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Name", login));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Password", password));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Account {0} has been created.", login);
            }
            catch (SqlException sqlException) when (sqlException.Number == 2627)
            {
                Console.WriteLine("Account {0} already exists.", login);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static DataSet Login()
        {
            Console.WriteLine("Insert login and password.");
            string login = Console.ReadLine();
            string password = Console.ReadLine();

            DataSet dataSet = new DataSet();

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
                        sqlCommand.CommandText = "mc.usp_GetParticipant_Id";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Name", login));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Password", password));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlConnection.State == ConnectionState.Closed)
                        {
                            sqlConnection.Open();
                        }

                        sqlDataAdapter.Fill(dataSet);

                        if (dataSet.Tables[0].Rows.Count != 0)
                        {
                            Console.WriteLine("Logged in as a user {0}.", dataSet.Tables[0].Rows[0]["Participant_Id"]);
                            return dataSet;
                        }
                        else
                        {
                            Console.WriteLine("Bad user name or password.");
                            return null;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }
    }
}
