using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japanesecastle1
{
    public class Delete
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True";

        public int DeleteData()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlDataAdapter = null;
            SqlTransaction sqlTransaction = null;
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
                builder.AppendLine("DELETE FROM japanesecastle where");
                builder.AppendLine("    id_num =");
                builder.AppendLine("    @idnum");

                sqlCommand = new SqlCommand(builder.ToString(), sqlConnection, sqlTransaction);

                SqlParameter para = sqlCommand.CreateParameter();
                para.ParameterName = "@idnum";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                string idnum1;
                int idnum2;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("削除したいデータのIDナンバーを入力してください");
                        idnum1 = Console.ReadLine();
                        idnum2 = int.Parse(idnum1);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("値が不正です");
                        continue;
                    }
                }
                para.Value = idnum2;
                sqlCommand.Parameters.Add(para);

                insert = sqlCommand.ExecuteNonQuery();

                sqlTransaction.Commit();
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Dispose();
            }

            return insert;
        }
    }
}
