using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Japanesecastle1
{
    public class Search
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True";
        public static DataTable AllSearch()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();

                string sql = "SELECT * FROM japanesecastle";

                sqlCommand = new SqlCommand(sql, sqlConnection);

                sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                sqlDataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return dataTable;
        }

        

        public static DataTable ChoiceSearch()
        {
            int insertrow = 0;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            SqlDataReader sqlDataReader = null;
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();

                sqlTransaction = sqlConnection.BeginTransaction();

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT * FROM japanesecastle");
                stringBuilder.AppendLine("  where   ");
                stringBuilder.AppendLine(" castle_name = @name");
                stringBuilder.AppendLine(" ,build_year = @buildyear");
                stringBuilder.AppendLine(" ,prefecture_name = @prefecturename");
                stringBuilder.AppendLine(" ,owner_name = @owner");
                stringBuilder.AppendLine(" ,important_grade = @important");
                stringBuilder.AppendLine(" ,defence_power_grade = @defence");
                stringBuilder.AppendLine(" ,exist_flg = @exist");

                sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection, sqlTransaction);

                SqlParameter para = sqlCommand.CreateParameter();
                para.ParameterName = "@name";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine();
                Console.WriteLine("城名を入力してください");
                string input1 = Console.ReadLine();
                para.Value = input1;
                sqlCommand.Parameters.Add(para);
                


                para = sqlCommand.CreateParameter();
                para.ParameterName = "@buildyear";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                int input2;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("築城年を入力してください");
                    try
                    {
                        input2 = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("不正な値です");
                        continue;
                    }
                }
                para.Value = input2;
                sqlCommand.Parameters.Add(para);
                insertrow = sqlCommand.ExecuteNonQuery();



                para = sqlCommand.CreateParameter();
                para.ParameterName = "@prefecturename";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine();
                Console.WriteLine("城の所在する都道府県名を入力してください");
                string input3 = Console.ReadLine();
                para.Value = input3;
                sqlCommand.Parameters.Add(para);
                insertrow = sqlCommand.ExecuteNonQuery();


                para = sqlCommand.CreateParameter();
                para.ParameterName = "@owner";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine();
                Console.WriteLine("建築人物名を入力してください");
                string input4 = Console.ReadLine();
                para.Value = input4;
                sqlCommand.Parameters.Add(para);
                insertrow = sqlCommand.ExecuteNonQuery();



                para = sqlCommand.CreateParameter();
                para.ParameterName = "@important";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                int input5;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("重要度ランクを入力してください");
                    try
                    {
                        input5 = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("不正な値です");
                        continue;
                    }
                }
                para.Value = input5;
                sqlCommand.Parameters.Add(para);
                insertrow = sqlCommand.ExecuteNonQuery();



                para = sqlCommand.CreateParameter();
                para.ParameterName = "@defence";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                int input6;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("防衛力ランクを入力してください");
                    try
                    {
                        input6 = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("不正な値です");
                        continue;
                    }
                }
                para.Value = input6;
                sqlCommand.Parameters.Add(para);
                insertrow = sqlCommand.ExecuteNonQuery();




                para = sqlCommand.CreateParameter();
                para.ParameterName = "@exist";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                int input7;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("現存フラグを入力してください");
                    Console.WriteLine("現存せず:0");
                    Console.WriteLine("現存:1");
                    try
                    {
                        input7 = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("不正な値です");
                        continue;
                    }
                }
                para.Value = input7;
                sqlCommand.Parameters.Add(para);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                
            }
            finally
            {
                sqlDataReader.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return dataTable;
    }
    }
}
