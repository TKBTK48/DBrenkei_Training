using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.CodeDom;

namespace WesternCastle1
{
    public class Search
    {
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "practice";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";


        public static DataTable ALLSearchsql()
        {
            SqlConnection sqlConDB = null;
            //SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataTable dataTable = new DataTable();

            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle";
                sqlCom = new SqlCommand(sql, sqlConDB);
                sqlDataAdapter = new SqlDataAdapter(sqlCom);
                sqlDataAdapter.Fill(dataTable);

            }
            finally
            {
                sqlDataAdapter.Dispose();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }
        public static DataTable NameChoiceSearchsql(string name)
        {
            SqlConnection sqlConDB = null;
            SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            //SqlDataAdapter sqlDataAdapter = null;
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();


            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle WHERE castle_name = @name";
                sqlCom = new SqlCommand(sql, sqlConDB, sqlTR);
                //sqlDataAdapter = new SqlDataAdapter(sqlCom);
                //sqlDataAdapter.Fill(dataTable);
                SqlParameter para = sqlCom.CreateParameter();
                para.ParameterName = "@name";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                para.Value = name;
                sqlCom.Parameters.Add(para);

                dataReader = sqlCom.ExecuteReader();
                dataTable.Load(dataReader);

            }
            finally
            {
                dataReader.Close();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }

        public static DataTable YearChoiceSearchsql(int year)
        {
            SqlConnection sqlConDB = null;
            SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            //SqlDataAdapter sqlDataAdapter = null;
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();


            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle WHERE build_year = @year";
                sqlCom = new SqlCommand(sql, sqlConDB, sqlTR);
                //sqlDataAdapter = new SqlDataAdapter(sqlCom);
                //sqlDataAdapter.Fill(dataTable);
                SqlParameter para = sqlCom.CreateParameter();
                para.ParameterName = "@year";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                para.Value = year;
                sqlCom.Parameters.Add(para);

                dataReader = sqlCom.ExecuteReader();
                dataTable.Load(dataReader);

            }
            finally
            {
                dataReader.Close();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }



        public static DataTable CountryChoiceSearchsql(string country)
        {
            SqlConnection sqlConDB = null;
            SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            //SqlDataAdapter sqlDataAdapter = null;
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();


            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle WHERE country_name = @country";
                sqlCom = new SqlCommand(sql, sqlConDB, sqlTR);
                //sqlDataAdapter = new SqlDataAdapter(sqlCom);
                //sqlDataAdapter.Fill(dataTable);
                SqlParameter para = sqlCom.CreateParameter();
                para.ParameterName = "@country";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                para.Value = country;
                sqlCom.Parameters.Add(para);

                dataReader = sqlCom.ExecuteReader();
                dataTable.Load(dataReader);

            }
            finally
            {
                dataReader.Close();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }



        public static DataTable OwnerChoiceSearchsql(string owner)
        {
            SqlConnection sqlConDB = null;
            SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            //SqlDataAdapter sqlDataAdapter = null;
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();


            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle WHERE owner_name = @owner";
                sqlCom = new SqlCommand(sql, sqlConDB, sqlTR);
                //sqlDataAdapter = new SqlDataAdapter(sqlCom);
                //sqlDataAdapter.Fill(dataTable);
                SqlParameter para = sqlCom.CreateParameter();
                para.ParameterName = "@owner";
                para.SqlDbType = SqlDbType.NVarChar;
                para.Direction = ParameterDirection.Input;
                para.Value = owner;
                sqlCom.Parameters.Add(para);

                dataReader = sqlCom.ExecuteReader();
                dataTable.Load(dataReader);

            }
            finally
            {
                dataReader.Close();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }



        public static DataTable ImportantChoiceSearchsql(int important)
        {
            SqlConnection sqlConDB = null;
            SqlTransaction sqlTR = null;
            SqlCommand sqlCom = null;
            //SqlDataAdapter sqlDataAdapter = null;
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();


            try
            {
                sqlConDB = new SqlConnection();
                sqlConDB.ConnectionString = DB_CONNECT;
                sqlConDB.Open();
                string sql = "SELECT * FROM westerncastle WHERE important_grade = @important";
                sqlCom = new SqlCommand(sql, sqlConDB, sqlTR);
                //sqlDataAdapter = new SqlDataAdapter(sqlCom);
                //sqlDataAdapter.Fill(dataTable);
                SqlParameter para = sqlCom.CreateParameter();
                para.ParameterName = "@important";
                para.SqlDbType = SqlDbType.Int;
                para.Direction = ParameterDirection.Input;
                para.Value = important;
                sqlCom.Parameters.Add(para);

                dataReader = sqlCom.ExecuteReader();
                dataTable.Load(dataReader);

            }
            finally
            {
                dataReader.Close();
                sqlCom.Dispose();
                sqlConDB.Close();
            }
            return dataTable;
        }
    }
}

