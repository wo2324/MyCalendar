using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TasksModule");
            Console.WriteLine("1. Show tasks");
            Console.WriteLine("2. Add task");
            Console.WriteLine("3. Edit task");
            Console.WriteLine("4. Delete task");
            int action = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insert Participant_Id");
            int Participant_Id = Convert.ToInt32(Console.ReadLine());
            int task_Id;
            switch (action)
            {
                case 1:
                    ShowTasks(Participant_Id);
                    break;
                case 2:
                    AddTask(Participant_Id);
                    break;
                case 3:
                    Console.WriteLine("Insert Task_Id");
                    task_Id = Convert.ToInt32(Console.ReadLine());
                    EditTask(Participant_Id, task_Id);
                    break;
                case 4:
                    Console.WriteLine("Insert Task_Id");
                    task_Id = Convert.ToInt32(Console.ReadLine());
                    DeleteTask(Participant_Id, task_Id);
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }

        static void ShowTasks(int participant_Id)
        {
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
                        sqlCommand.CommandText = "mc.usp_TasksGet";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Participant_Id", participant_Id));
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlConnection.State == ConnectionState.Closed)
                        {
                            sqlConnection.Open();
                        }

                        sqlDataAdapter.Fill(dataSet);

                        if (dataSet != null)
                        {
                            foreach (DataTable dataTable in dataSet.Tables)
                            {
                                foreach (DataRow dataRow in dataTable.Rows)
                                {
                                    string task_Id = dataRow["Task_Id"].ToString();
                                    string task_Name = dataRow["Task_Name"].ToString();
                                    string task_Description = dataRow["Task_Description"].ToString();

                                    Console.WriteLine("{0} - {1} - {2}", task_Id, task_Name, task_Description);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void AddTask(int participant_Id)
        {
            Console.WriteLine("Insert task name and description.");
            string name = Console.ReadLine();
            string description = Console.ReadLine();

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
                        sqlCommand.CommandText = "mc.usp_TaskAdd";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Name", name));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Description", description));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Participant_Id", participant_Id));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Task {0} has been created.", name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void EditTask(int participant_Id, int task_Id)
        {
            Console.WriteLine("Insert task name and description.");
            string name = Console.ReadLine();
            string description = Console.ReadLine();

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
                        sqlCommand.CommandText = "mc.usp_TaskEdit";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Id", task_Id));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Name", name));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Description", description));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Participant_Id", participant_Id));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Task {0} has been edit.", name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void DeleteTask(int participant_Id, int task_Id)
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
                        sqlCommand.CommandText = "mc.usp_TaskDelete";
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Id", task_Id));
                        sqlCommand.Parameters.Add(new SqlParameter("@p_Task_Participant_Id", participant_Id));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Task {0} has been removed.", task_Id);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
