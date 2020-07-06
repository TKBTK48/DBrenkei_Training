using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Itword.Dto;

namespace ITword.Daos
{
    public class BaseDao
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "Dictionary";

        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";


        public static DataTable GoSql()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();

                sqlTransaction = sqlConnection.BeginTransaction();

                string sql = "SELECT * FROM itword";


                sqlCommand = new SqlCommand(sql, sqlConnection, sqlTransaction);

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


        public  static void Setpara(SqlCommand activecommand, string ParaName, string value, SqlDbType sqlDbType)
        {
            SqlParameter parameter = activecommand.CreateParameter();
            parameter.ParameterName = ParaName;
            parameter.SqlDbType = sqlDbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = value;
            activecommand.Parameters.Add(parameter);
        }


        public static void Insertsql()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand activecommand = null;
            DataTable dataTable = new DataTable();
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                var sqlsentence = new StringBuilder();
                sqlsentence.AppendLine("INSERT INTO "+"itword");
                sqlsentence.AppendLine(" (word_name,");
                sqlsentence.AppendLine(" system_no1,");
                sqlsentence.AppendLine(" system_no2,");
                sqlsentence.AppendLine(" system_no3,");
                sqlsentence.AppendLine(" detail,");
                sqlsentence.AppendLine(" recordtime");
                sqlsentence.AppendLine(") VALUES");
                sqlsentence.AppendLine(" (@word,");
                sqlsentence.AppendLine(" @s1,");
                sqlsentence.AppendLine(" @s2,");
                sqlsentence.AppendLine(" @s3,");
                sqlsentence.AppendLine(" @detail,");
                sqlsentence.AppendLine(" @time");
                sqlsentence.AppendLine(");");

                activecommand = new SqlCommand(sqlsentence.ToString(), sqlConnection);
                activecommand.Transaction = sqlTransaction;
                var tabledto = new TableDto();

                Console.WriteLine("ワードを入力してください");
                tabledto.Name = Console.ReadLine();
                Setpara(activecommand, "@word",tabledto.Name, SqlDbType.Char);


                Console.WriteLine("関連システム１を入力してください");
                tabledto.Sys1 = Console.ReadLine();
                Setpara(activecommand, "@s1", tabledto.Sys1, SqlDbType.Char);

                Console.WriteLine("関連システム２を入力してください");
                tabledto.Sys2 = Console.ReadLine();
                Setpara(activecommand, "@s2", tabledto.Sys2, SqlDbType.Char);

                Console.WriteLine("関連システム３を入力してください");
                tabledto.Sys3 = Console.ReadLine();
                Setpara(activecommand, "@s3", tabledto.Sys3, SqlDbType.Char);

                Console.WriteLine("詳細メモを入力してください");
                tabledto.Detail = Console.ReadLine();
                Setpara(activecommand, "@detail", tabledto.Detail, SqlDbType.Char);

                tabledto.Record = DateTime.Now;
                Setpara(activecommand, "@time", tabledto.Record.ToString(), SqlDbType.Char);


                activecommand.ExecuteNonQuery();
                sqlTransaction.Commit();


            }
            finally
            {
                activecommand.Dispose();
                sqlConnection.Close();
            }
        }




    }
}
