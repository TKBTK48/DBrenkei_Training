using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EMSystem_CUI.Daos
{
    public abstract class BaseDao<T>
    {
        protected SqlConnection Con { get; private set; }
        protected SqlTransaction SqlTran { get; private set; }

        protected BaseDao(SqlConnection Con)
        {
            this.Con = Con;
        }
        protected BaseDao(SqlConnection Con, SqlTransaction SqlTran)
        {
            this.Con = Con;
            this.SqlTran = SqlTran;
        }

        /// <summary>
        /// 全件検索
        /// </summary>
        /// <returns>取得した結果 T型のリスト</returns>
        public List<T> SelectAll()
        {
            SqlCommand cmd = null;
            List<T> retList = null;

            try
            {
                string sql = $@"
                    SELECT
                        *
                    FROM
                        {this.GetTableName()}
                ";
                cmd = new SqlCommand(sql, this.Con);

                retList = ExecuteSelectSql(cmd);
            }
            finally
            {
                cmd.Dispose();
            }

            // 検索結果をリターン
            return retList;
        }

        /// <summary>
        /// ID検索
        /// </summary>
        /// <param name="id">検索するID</param>
        /// <returns>取得した結果 T型のリスト</returns>
        public List<T> SelectById(int id)
        {
            List<T> list = null;

            /*
             * 逐次文字列で作成
             * 見やすいが、処理を挟む場合はStringBuilderが良い
             */
            string sql = $@"
                SELECT
                    *
                FROM
                    {this.GetTableName()}
                WHERE
                    {this.GetId()} = @id
            ";

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, this.Con);
                SetParameter(cmd, "@id", id);
                list = ExecuteSelectSql(cmd);
                return list;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// SELECT文を実行し取得した結果をリストに詰めて返す
        /// </summary>
        /// <param name="cmd">実行するSqlCommand</param>
        /// <returns>取得した結果 T型のリスト</returns>
        public List<T> ExecuteSelectSql(SqlCommand cmd)
        {
            SqlDataReader reader = null;
            List<T> list = new List<T>();

            try
            {
                // SQL実行
                reader = cmd.ExecuteReader();
                list = this.CreateEntityList(reader);
            }
            finally
            {
                reader.Close();
                cmd.Dispose();
            }

            return list;
        }

        /// <summary>
        /// DataTableの値をEmployee型のリストにして返す
        /// </summary>
        /// <param name="table">DBから取得したDataTable</param>
        /// <returns>取得した結果 T型のリスト</returns>
        public List<T> CreateEntityList(SqlDataReader reader)
        {
            List<T> dtoList = new List<T>();

            // Listに詰めなおし
            while (reader.Read())
            {
                dtoList.Add(this.RowMapping(reader));
            }

            return dtoList;
        }

        /// <summary>
        /// SQL文のパラメータセット
        /// </summary>
        /// <param name="cmd">セットするSqlCommand</param>
        /// <param name="ParamName">パラメータ名</param>
        /// <param name="value">文字列</param>
        public static void SetParameter(SqlCommand cmd, string ParamName, string value)
        {
            SqlParameter param = cmd.CreateParameter();
            param.ParameterName = ParamName;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Direction = ParameterDirection.Input;
            param.Value = value;
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// SQL文のパラメータセット
        /// </summary>
        /// <param name="cmd">セットするSqlCommand</param>
        /// <param name="ParamName">パラメータ名</param>
        /// <param name="value">数値</param>
        public static void SetParameter(SqlCommand cmd, string ParamName, int value)
        {
            SqlParameter param = cmd.CreateParameter();
            param.ParameterName = ParamName;
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.Input;
            param.Value = value;
            cmd.Parameters.Add(param);
        }

        public abstract string GetTableName();
        public abstract string GetId();

        protected abstract T RowMapping(SqlDataReader reader);
    }
}
