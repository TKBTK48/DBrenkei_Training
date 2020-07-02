using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Japanesecastle1
{
    public class Insert
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True";

        public int InsertData()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlTransaction sqlTransaction = null;
            DataTable dataTable = new DataTable();
            int insert = 0;


            try
            {
                sqlConnection = new SqlConnection
                {
                    ConnectionString = DB_CONNECT
                };
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("INSERT INTO japanesecastle(");
                builder.AppendLine("   castle_name");
                builder.AppendLine("   ,build_year");
                builder.AppendLine("   ,prefecture_name");
                builder.AppendLine("   ,owner_name");
                builder.AppendLine("   ,important_grade");
                builder.AppendLine("   ,defence_power_grade");
                builder.AppendLine("   ,exist_flg");
                builder.AppendLine(" ) VALUES (");
                builder.AppendLine("   @name");
                builder.AppendLine("   ,@buildyear");
                builder.AppendLine("   ,@prefecture");
                builder.AppendLine("   ,@owner");
                builder.AppendLine("   ,@important");
                builder.AppendLine("   ,@defence");
                builder.AppendLine("   ,@exist");
                builder.AppendLine(" )");


                sqlCommand = new SqlCommand(builder.ToString(), sqlConnection, sqlTransaction);

                SqlParameter para = sqlCommand.CreateParameter();
                para.ParameterName = "@name";
                para.SqlDbType = SqlDbType.NChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("城名を入力してください");
                string intname = Console.ReadLine();
                para.Value = intname;
                sqlCommand.Parameters.Add(para);


                para = sqlCommand.CreateParameter();
                para.ParameterName = "@buildyear";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;

                string intyear1;
                int intyear2;

                while (true)
                {
                    Console.WriteLine("築城年を入力してください");
                    intyear1 = Console.ReadLine();
                    try
                    {
                        intyear2 = int.Parse(intyear1);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("値が不正です");
                        continue;
                    }
                }

                para.Value = intyear2;
                sqlCommand.Parameters.Add(para);




                para = sqlCommand.CreateParameter();
                para.ParameterName = "@prefecture";
                para.SqlDbType = SqlDbType.NChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("所在都道府県を入力してください");
                para.Value = Console.ReadLine();
                sqlCommand.Parameters.Add(para);



                para = sqlCommand.CreateParameter();
                para.ParameterName = "@owner";
                para.SqlDbType = SqlDbType.NChar;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("築城人物名を入力してください");
                para.Value = Console.ReadLine();
                sqlCommand.Parameters.Add(para);



                para = sqlCommand.CreateParameter();
                para.ParameterName = "@important";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("重要度ランクを入力してください");
                var numbercheck4 = new NumberCheck();
                int choice4 = numbercheck4.NumberChecker(5);
                para.Value = choice4;
                sqlCommand.Parameters.Add(para);

                para = sqlCommand.CreateParameter();
                para.ParameterName = "@defence";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("防衛ランクを入力してください");
                var numbercheck5 = new NumberCheck();
                int choice5 = numbercheck5.NumberChecker(5);
                para.Value = choice5;
                sqlCommand.Parameters.Add(para);


                para = sqlCommand.CreateParameter();
                para.ParameterName = "@exist";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                Console.WriteLine("現存フラグを入力してください");
                Console.WriteLine("0:現存せず");
                Console.WriteLine("1:現存");
                var numbercheck6 = new NumberCheck();
                int choice6 = numbercheck6.NumberChecker(1);
                para.Value = choice6;
                sqlCommand.Parameters.Add(para);

                insert = sqlCommand.ExecuteNonQuery();

                sqlTransaction.Commit();
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return insert;
        }

    }
}
