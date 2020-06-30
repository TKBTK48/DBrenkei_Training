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
    class DataDelete
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";
        public int DataDeletetool()
        {
            int insertRow = 0;
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlCommand sqlCommand = null;
            DataTable dataTable = new DataTable();
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("DELETE FROM westerncastle");
                stringBuilder.AppendLine("    where");
                stringBuilder.AppendLine("    id_num");
                stringBuilder.AppendLine("    =     ");
                stringBuilder.AppendLine("    @id");

                sqlCommand = new SqlCommand(stringBuilder.ToString(), sqlConnection, sqlTransaction);

                // @idパラメータに設定
                SqlParameter sqlParameter = sqlCommand.CreateParameter();
                Console.WriteLine("ID番号を入力してください");
                Console.WriteLine();
                var casecheck7 = new Casenumbercheck();
                casecheck7.Casenumberchecker4();
                int case7 = casecheck7.case4;
                string casenum5 = casecheck7.casenum4;
                int inputnum = case7;
                sqlParameter.ParameterName = "@id";
                sqlParameter.SqlDbType = SqlDbType.Int;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.Value = case7;
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
