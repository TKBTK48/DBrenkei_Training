using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DbAccess.Utils
{
    public static class DBUtil
    {
        // 自分の環境化に変えてください
        private const string SERVER = "localhost";
        private const string DB_INSTANCE = "SQLEXPRESS";
        //private const string DB_USER = "emdb_user";
        //private const string DB_PASS = "12qwaszx";
        private const string DB_NAME = "emdb";
        private const string WINDOWS_AUTH_OPTION = "Integrated Security=True";

        private static SqlConnection con;

        public static SqlConnection GetConnection()
        {
            string dbConnectionStr = $@"Server={SERVER}\{DB_INSTANCE}" +
                                        $"; Database={DB_NAME}; {WINDOWS_AUTH_OPTION}";
            try
            {
                con = new SqlConnection()
                {
                    ConnectionString = dbConnectionStr
                };
                con.Open();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return con;
        }

        public static void CloseConnection(SqlConnection sqlCon)
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
            }
        }
    }
}