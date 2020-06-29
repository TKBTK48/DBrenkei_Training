using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;

namespace WesternCastle1
{
    public class DataAdd
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";
        public int DataAddtool()
        {
            int insertRow = 0;
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;
            DataTable dataTable = new DataTable();
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("INSERT INTO westerncastle(");
                stringBuilder.AppendLine("    castle_name");
                stringBuilder.AppendLine("    ,build_year");
                stringBuilder.AppendLine("    ,country_name");
                stringBuilder.AppendLine("    ,owner_name");
                stringBuilder.AppendLine("    ,important_grade");
                stringBuilder.AppendLine(" ) VALUES (");
                stringBuilder.AppendLine("    @name");
                stringBuilder.AppendLine("    ,@buildyear");
                stringBuilder.AppendLine("    ,@country");
                stringBuilder.AppendLine("    ,@owner");
                stringBuilder.AppendLine("    ,@important");
                stringBuilder.AppendLine(" )");

                sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection, sqlTransaction);

                // @nameパラメータに設定
                Console.WriteLine("城名を入力してください");
                Console.WriteLine();
                SqlParameter sqlParameter = sqlCommand.CreateParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.SqlDbType = SqlDbType.NChar;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = Console.ReadLine();
                sqlCommand.Parameters.Add(sqlParameter);

                // @buildyearパラメータに設定
                sqlParameter = sqlCommand.CreateParameter();
                Console.WriteLine("築城年を入力してください");
                Console.WriteLine();
                var casecheck5 = new Casenumbercheck();
                casecheck5.Casenumberchecker4();
                int case5 = casecheck5.case4;
                string casenum5 = casecheck5.casenum4;
                int inputnum = case5;
                sqlParameter.ParameterName = "@buildyear";
                sqlParameter.SqlDbType = SqlDbType.Int;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = case5;
                sqlCommand.Parameters.Add(sqlParameter);

                // @countryパラメータに設定
                sqlParameter = sqlCommand.CreateParameter();
                Console.WriteLine("国名を入力してください");
                Console.WriteLine();
                sqlParameter.ParameterName = "@country";
                sqlParameter.SqlDbType = SqlDbType.NChar;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = Console.ReadLine();
                sqlCommand.Parameters.Add(sqlParameter);

                // @ownerパラメータに設定
                sqlParameter = sqlCommand.CreateParameter();
                Console.WriteLine("築城人物名を入力してください");
                Console.WriteLine();
                sqlParameter.ParameterName = "@owner";
                sqlParameter.SqlDbType = SqlDbType.NChar;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = Console.ReadLine();
                sqlCommand.Parameters.Add(sqlParameter);

                // @importantパラメータに設定
                sqlParameter = sqlCommand.CreateParameter();
                Console.WriteLine("重要度ランクを入力してください");
                Console.WriteLine();
                var casecheck6 = new Casenumbercheck();
                casecheck6.Casenumberchecker4();
                int case6 = casecheck6.case4;
                string casenum6 = casecheck6.casenum4;
                int inputnum2 = case6;
                sqlParameter.ParameterName = "@important";
                sqlParameter.SqlDbType = SqlDbType.Int;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = case6;
                sqlCommand.Parameters.Add(sqlParameter);

                insertRow = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return insertRow;
        }
    }
}
