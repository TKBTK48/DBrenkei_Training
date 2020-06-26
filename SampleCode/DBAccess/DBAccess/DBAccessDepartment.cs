using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ※DataSetを使う場合は以下のusingが必要
using System.Data;
/*
 *=======================================================
 * ①データベース接続用プログラムは以下のusingが必要
 *=======================================================
 */
using System.Data.SqlClient;

namespace DBAccess
{
    class DBAccessDepartment
    {
        // DBサーバーの場所・DBの名前・DBの定義をそれぞれ変数に格納
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "emdb";
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";

        /// <summary>
        /// employeeテーブル全件検索用
        /// </summary>
        /// <returns>DataTable型の取得結果</returns>
        public static DataTable ExecuteSql()
        {
            SqlConnection sqlCon = null;
            // トランザクション
            //SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter dataAdapter = null;
            // 取得データ保存用のdataset宣言
            DataTable dtResult = new DataTable();

            try
            {
                /*
                 *=======================================================
                 * ②データベース接続
                 *  1. SqlConnection型を用意
                 * 接続文字列をConnectionString
                 *=======================================================
                 */
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = DB_CONNECT;
                sqlCon.Open();

                // トランザクション開始
                //sqlTran = sqlCon.BeginTransaction();

                /*
                 *=======================================================
                 * ③SQL用意（ここではただの文字列）
                 *=======================================================
                 */
                string sql = "SELECT * FROM department";

                /*
                 *=======================================================
                 * ④SQL実行
                 *  1. SqlCommand(SQL文、SqlConnection型のインスタンス)作成
                 *=======================================================
                 */
                sqlCommand = new SqlCommand(sql, sqlCon);
                // トランザクションを実行するならこっち
                //sqlCommand = new SqlCommand(sql, sqlCon, sqlTran);

                /*
                 *=======================================================
                 * ⑤実行した結果を取得
                 *  1. SqlDataAdapter型のインスタンスを作成
                 *     SqlDataAdapterは一度にデータをごそっと取得して処理する
                 *  2. Fillメソッドを実行し、DataTable型に格納
                 *  　 DataTable型 = メモリ上にテーブルを構成するクラス
                 *  　 System.Data.DataSetクラス        （＝DB）
                 *      ┗System.Data.DataTableクラス   （＝テーブル）
                 *        ┣System.DataColumnクラス     （＝列・カラム）
                 *        ┗System.DataRowクラス        （＝行・レコード）
                 *=======================================================
                 */
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dtResult);

                // コミット処理(更新系は必要)
                //sqlTran.Commit();
            }
            finally
            {
                /*
                 *=======================================================
                 * ⑤後始末処理
                 *  1. SqlDataAdapterの廃棄
                 *  2. SqlCommandの廃棄
                 *  3. SqlConnectionの切断
                 *=======================================================
                 */
                dataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlCon.Close();
            }

            return dtResult;
        }

        /// <summary>
        /// employeeテーブル mail_address検索用
        /// </summary>
        /// <returns>DataTable型の取得結果</returns>
        public static DataTable ExceuteSqlInjection(string department)
        {
            SqlConnection sqlCon = null;
            SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;
            SqlDataAdapter dataAdapter = null;
            // 取得データ保存用のdataset宣言
            DataTable dtResult = new DataTable();

            try
            {
                // データベース接続
                sqlCon = new SqlConnection
                {
                    ConnectionString = DB_CONNECT
                };
                sqlCon.Open();

                // トランザクション開始
                sqlTran = sqlCon.BeginTransaction();

                // SQL定義
                string sql = $"SELECT * FROM department WHERE nm_department = '{department}'";
                // SQL実行
                sqlCommand = new SqlCommand(sql, sqlCon, sqlTran);
                // SQL serverにあるSQL実行結果とC#をつなげる役割
                dataAdapter = new SqlDataAdapter(sqlCommand);
                // 結果を保持
                dataAdapter.Fill(dtResult);
            }
            finally
            {
                // トランザクション終了
                sqlTran.Commit();
                dataAdapter.Dispose();
                sqlCommand.Dispose();
                sqlCon.Close();
            }

            //結果を返す
            return dtResult;
        }

        /// <summary>
        /// employeeテーブル mail_address検索用
        /// SQLインジェクション対策版
        /// </summary>
        /// <returns>DataTable型の取得結果</returns>
        public static DataTable ExceuteSqlByDepartment(string department)
        {
            SqlConnection sqlCon = null;
            SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;
            SqlDataReader reader = null;
            // 取得データ保存用のdataset宣言
            DataTable dtResult = new DataTable();

            try
            {
                // データベース接続
                sqlCon = new SqlConnection
                {
                    ConnectionString = DB_CONNECT
                };
                sqlCon.Open();

                // トランザクション開始
                sqlTran = sqlCon.BeginTransaction();

                // SQL定義
                // @param を使用
                string sql = "SELECT * FROM department WHERE nm_department = @department";
                // 条件の設定
                sqlCommand = new SqlCommand(sql, sqlCon, sqlTran);
                SqlParameter param = sqlCommand.CreateParameter();
                param.ParameterName = "@department";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Direction = ParameterDirection.Input;
                param.Value = department;
                sqlCommand.Parameters.Add(param);

                // SQL実行
                /*
                 * ExecuteReaderはSqlDataAdapterとは違い、
                 * 一行一行読み込んで処理できる
                 */
                reader = sqlCommand.ExecuteReader();
                dtResult.Load(reader);
            }
            finally
            {
                reader.Close();
                sqlCommand.Dispose();
                sqlCon.Close();
            }

            return dtResult;
        }
    }
}
