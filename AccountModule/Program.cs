using System;
using System.Collections.Generic;
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
            Console.WriteLine("MyCalendar");
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

            string connectionString = @"Server=localhost;Database=MyCalendar;Trusted_Connection=True";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "usp_CalendarUserAdd";

                    sqlCommand.Parameters.Add(new SqlParameter("@p_Name", login));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Password", password));

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        static void Login()
        {
            Console.WriteLine("Insert login and password.");
            string login = Console.ReadLine();
            string password = Console.ReadLine();

            string connectionString = @"Server=localhost;Database=MyCalendar;Trusted_Connection=True";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "usp_GetCalendarUser_Id";

                    sqlCommand.Parameters.Add(new SqlParameter("@p_Name", login));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Password", password));

                    sqlCommand.ExecuteNonQuery();
                    //odbieranie danych
                }
            }
        }
    }
}
