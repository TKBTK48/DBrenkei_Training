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

        public int insertrow = 0;

        public static DataTable ChoiceSearch()
        {
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



                insertrow = sqlCommand.ExecuteNonQuery();

            }
            finally
            {

            }
    }
    }
}
