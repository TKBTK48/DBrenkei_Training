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
using ITword.Main;

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


        public static void Setpara(SqlCommand activecommand, string ParaName, string value, SqlDbType sqlDbType)
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
                sqlsentence.AppendLine("INSERT INTO " + "itword");
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
                Setpara(activecommand, "@word", tabledto.Name, SqlDbType.Char);


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


        public static DataTable Filtering()
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
                var sb = new StringBuilder();
                sb.AppendLine("SELECT * FROM itword WHERE ");
                Console.WriteLine("検索に使用する条件を選択してください");
                Console.WriteLine("0:ワード");
                Console.WriteLine("1:関連システム");
                Console.WriteLine("2:詳細メモ");
                var tabledto2 = new TableDto();
                var check3 = new INTcheck();
                int case3 = check3.INTchecker(2);
                if (case3 == 0)
                {
                    sb.AppendLine("word_name = ");
                    sb.AppendLine("@name");
                    sqlCommand = new SqlCommand(sb.ToString(), sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Console.WriteLine("キーワードを入力してください");
                    tabledto2.Name = Console.ReadLine();
                    Setpara(sqlCommand, "@name", tabledto2.Name, SqlDbType.NVarChar);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                }
                else if (case3 == 1)
                {

                }
                else
                {

                }

            }
            finally
            {
                sqlDataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return dataTable;
        }

        public static void Update()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                Console.WriteLine("更新したいワードのIDを入力してください");
                int input_idnum = int.Parse(Console.ReadLine());
                var sb = new StringBuilder();
                sb.AppendLine("UPDATE itword SET ");

                Console.WriteLine("更新したい項目を以下から選び数値を入力してください");
                Console.WriteLine("0:ワード");
                Console.WriteLine("1:関連システム１");
                Console.WriteLine("2:関連システム２");
                Console.WriteLine("3:関連システム３");
                Console.WriteLine("4:詳細メモ");

                var check5 = new INTcheck();
                int case5 = check5.INTchecker(4);
                var tabledto3 = new TableDto();


                if (case5 == 0)
                {
                    sb.AppendLine("word_name = ");
                    sb.AppendLine("@word");
                    sb.AppendLine($" WHERE id_num = '{input_idnum}'");
                    string sql = sb.ToString();
                    Console.WriteLine("新しいワードを入力してください");
                    tabledto3.Name = Console.ReadLine();
                    sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@word", tabledto3.Name, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                }

                else if (case5 == 1)
                {
                    sb.AppendLine("system_no1 = ");
                    sb.AppendLine("@sys1");
                    sb.AppendLine($" WHERE id_num = '{input_idnum}'");
                    string sql = sb.ToString();
                    Console.WriteLine("新しい関連システム１を入力してください");
                    tabledto3.Sys1 = Console.ReadLine();
                    sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@sys1", tabledto3.Sys1, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                }
                else if (case5 == 2)
                {
                    sb.AppendLine("system_no2 = ");
                    sb.AppendLine("@sys2");
                    sb.AppendLine($" WHERE id_num = '{input_idnum}'");
                    string sql = sb.ToString();
                    Console.WriteLine("新しい関連システム２を入力してください");
                    tabledto3.Sys2 = Console.ReadLine();
                    sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@sys2", tabledto3.Sys2, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                else if (case5 == 3)
                {
                    sb.AppendLine("system_no3 = ");
                    sb.AppendLine("@sys3");
                    sb.AppendLine($" WHERE id_num = '{input_idnum}'");
                    string sql = sb.ToString();
                    Console.WriteLine("新しい関連システム３を入力してください");
                    tabledto3.Sys3 = Console.ReadLine();
                    sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@sys3", tabledto3.Sys3, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                else if (case5 == 4)
                {
                    sb.AppendLine("detail = ");
                    sb.AppendLine("@detail");
                    sb.AppendLine($" WHERE id_num = '{input_idnum}'");
                    string sql = sb.ToString();
                    Console.WriteLine("新しい詳細メモを入力してください");
                    tabledto3.Detail = Console.ReadLine();
                    sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@detail", tabledto3.Detail, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }

            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public static void Deletesql()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                var sb = new StringBuilder();
                sb.AppendLine("DELETE itword where ");

                Console.WriteLine("削除に使用する条件を選択してください");
                Console.WriteLine("0:ID");
                Console.WriteLine("1:ワード");

                var check6 = new INTcheck();
                int input6 = check6.INTchecker(1);

                if (input6 == 0)
                {
                    Console.WriteLine("削除したいワードのIDを入力してください");
                    int delete_num = int.Parse(Console.ReadLine());
                    sb.AppendLine($"id_num =");
                    sb.AppendLine($" '{delete_num}'");
                    sqlCommand = new SqlCommand(sb.ToString(), sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                }
                else
                {
                    sb.AppendLine("word_name = ");
                    sb.AppendLine("@name");
                    Console.WriteLine("削除したいワードを入力してください");
                    string inputdelete = Console.ReadLine();
                    var tabledto7 = new TableDto();
                    tabledto7.Name = inputdelete;
                    
                    sqlCommand = new SqlCommand(sb.ToString(),sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    Setpara(sqlCommand, "@name", tabledto7.Name, SqlDbType.NVarChar);
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }


            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public static int Countrow()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;
            DataTable data = new DataTable();
            int row;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                var sb = new StringBuilder();
                sb.AppendLine("SELECT *");
                sb.AppendLine(" FROM itword");
                sqlCommand = new SqlCommand(sb.ToString(), sqlConnection);
                sqlCommand.Transaction = sqlTransaction;
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(data);
                row = data.Rows.Count;
            }
            finally
            {
                sqlDataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return row;
        }

        public static int Max()
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;
            DataTable data = new DataTable();
            int result;
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = DB_CONNECT;
                sqlConnection.Open();
                sqlTransaction = sqlConnection.BeginTransaction();
                var sb = new StringBuilder();
                sb.AppendLine("SELECT ");
                sb.AppendLine("MAX(id_num) ");
                sb.AppendLine("FROM itword");
                sqlCommand = new SqlCommand(sb.ToString(), sqlConnection);
                sqlCommand.Transaction = sqlTransaction;
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(data);
                DataRowCollection row = data.Rows;
                int a = 0;
                int b = 0;                
                result = int.Parse(row[a][b].ToString());
            }
            finally
            {
                sqlDataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return result;
        }

        public static void Learnsql(int row)
        {
            SqlConnection sqlConnection = null;
            SqlTransaction sqlTransaction = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlCommand sqlCommand = null;           
            DataRowCollection row2;

            var data = new DataTable();
            while (true)
            {
                Random r1 = new System.Random();
                int r2 = r1.Next(1, row + 1);
                try
                {
                    sqlConnection = new SqlConnection();
                    sqlConnection.ConnectionString = DB_CONNECT;
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();
                    var sb = new StringBuilder();
                    sb.AppendLine("SELECT *");
                    sb.AppendLine(" FROM itword");
                    sb.AppendLine(" WHERE id_num = ");
                    sb.AppendLine("@idnum");
                    sqlCommand = new SqlCommand(sb.ToString(), sqlConnection);
                    sqlCommand.Transaction = sqlTransaction;
                    SqlParameter parameter = sqlCommand.CreateParameter();
                    parameter.ParameterName = "@idnum";
                    parameter.SqlDbType = SqlDbType.Int;
                    parameter.Direction = ParameterDirection.Input;
                    parameter.Value = r2;
                    sqlCommand.Parameters.Add(parameter);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(data);
                }
                finally
                {
                    sqlDataAdapter.Dispose();
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
                row2 = data.Rows;
                if (row2.Count != 0)
                {
                    break;
                    
                }
                else
                {
                    continue;
                }
            }
            DataColumnCollection columns = data.Columns;
            Console.WriteLine(row2.Count);
            for (int r = 0; r < row2.Count; r++)
            {
                int c = 5;
                Console.WriteLine("以下の詳細ワードは何のことですか？");
                Console.WriteLine(row2[r][c]);
                string input = Console.ReadLine();
                int c1 = 1;
                string seikai = row2[r][c1].ToString();
                if (seikai == input)
                {
                    Console.WriteLine("正解です");
                    Console.WriteLine(row2[r][c1]);
                }
                else
                {
                    Console.WriteLine("間違いです");
                    Console.WriteLine(row2[r][c1]);
                }
            }
        }


    }
}
