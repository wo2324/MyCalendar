using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar.Utils
{
    public static class DbHandler
    {
        public static int ParticipantIdGet(string login, string password)
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

        public static void CreateAccount(string login, string password)
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

        public static void EditPassword(int Participant_Id, string newPassword)
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

        public static DataSet GetPlannerName(int participantId)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_Planner_NameGet";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Participant_Id", participantId));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }

        public static void CreatePlanner(string plannerName, string plannerDescription, int participantId)    //walidacja
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_PlannerAdd";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Description", plannerDescription));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Participant_Id", participantId));
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static DataSet GetTask(string plannerName, int participantId)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionStirng"].ToString();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "mc.usp_TaskGet";
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Name", plannerName));
                    sqlCommand.Parameters.Add(new SqlParameter("@p_Planner_Participant_Id", participantId));
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }
    }
}
