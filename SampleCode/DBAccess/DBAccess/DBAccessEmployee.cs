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
    class DBAccessEmployee
    {
        // DBサーバーの場所・DBの名前・DBの定義をそれぞれ変数に格納
        private static string DB_HOST = "localhost\\SQLEXPRESS";
        private static string DB_NAME = "emdb";

        //①サーバ/インスタンス指定　②データベース指定　③認証方法
        //「Integrated Security=True」はWindows認証を使うという指定
        private static string DB_CONNECT = $"Server={DB_HOST}; Database={DB_NAME}; Integrated Security=True;";

        /// <summary>
        /// employeeテーブル全件検索用
        /// </summary>
        /// <returns>DataTable型の取得結果</returns>
        public static DataTable ExecuteSql()
        {
            //必要な型の変数宣言
            SqlConnection sqlCon = null;    //データベース接続用
            // トランザクション
            //SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;   //SQL実行用
            SqlDataAdapter dataAdapter = null;  //実行結果データ取得用
            // 取得データ保存用のdataset宣言
            DataTable dtResult = new DataTable();   //実行結果データ格納用

            try
            {
                /*
                 *=======================================================
                 * ②データベース接続
                 *  1. SqlConnection型を用意
                 * 接続文字列をConnectionString
                 *  2. Openメソッドを呼ぶ
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
                string sql = "SELECT * FROM employee";

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
                 * ⑥後始末処理
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
        public static DataTable ExceuteSqlInjection(string mail)
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
                //完全一致
                //string sql = $"SELECT * FROM employee WHERE mail_address = '{mail}'";
                //あいまい
                string sql = $"SELECT * FROM employee WHERE mail_address like '%{mail}%'";

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
        public static DataTable ExceuteSqlByMail(string mail)
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
                string sql = "SELECT * FROM employee WHERE mail_address = @mail";
                sqlCommand = new SqlCommand(sql, sqlCon, sqlTran);
                /*
                 * 条件の設定、パラメータ設定
                 *  1. SqlCommand.CreateParameterメソッドを実行し、SqlParameterインスタンスを作成
                 *  2. SqlParameterインスタンスのプロパティ設定
                 *     ParameterName ・・・ パラメータ名 SQL内での「@～」（「@～」は任意に付けられる）
                 *     SqlDbType ・・・ 型を指定 文字列の場合は、NCharかNVarChar 基本はNCharを使えばいい
                 *     Direction ・・・ データの方向 入力だったらInput
                 *     Value ・・・ 実際にSQLに当てはめる値
                 *  3. SqlCommand.Parametersに作ったSqlParameterインスタンスを追加
                 */
                SqlParameter param = sqlCommand.CreateParameter();
                param.ParameterName = "@mail";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Direction = ParameterDirection.Input;
                param.Value = mail;
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

        public static int InsertEmployee()
        {
            SqlConnection sqlCon = null;
            SqlTransaction sqlTran = null;
            SqlCommand sqlCommand = null;
            int insertRowCount = 0;

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

                // 方法① +で結合する
                // 処理も遅いし、容量使うので避ける
                /*                string sql = "INSERT INTO employee("
                                        + "nm_employee"
                                        + "kn_employee"
                                        + ",mail_address"
                                        + ",password"
                                        + ", id_department";*/

                // SQL作成方法②　StringBuilderクラスを使う
                // 処理は早い、可読性も少し上がる、途中処理を入れることもOK
                /*
                 * 1. StringBuilerインスタンスを生成
                 * 2. Appendメソッドを使って結合する(改行したい場合はAppendLine)
                 * 3. 結合した文字列を使いたい場合は、ToStringメソッドを実行
                 */
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" INSERT INTO employee(");
                sb.AppendLine("    nm_employee");
                sb.AppendLine("    ,kn_employee");
                sb.AppendLine("    ,mail_address");
                sb.AppendLine("    ,password");
                sb.AppendLine("    ,id_department");
                sb.AppendLine(" ) VALUES (");
                sb.AppendLine("    @name");
                sb.AppendLine("    ,@kana");
                sb.AppendLine("    ,@mail");
                sb.AppendLine("    ,@pass");
                sb.AppendLine("    ,@idDep");
                sb.AppendLine(" )");

                // SQL方法③逐語的文字列を使う(@"") 途中で変数入れたい場合は($@"")
                /* 
                 * 可読性が一番、処理も早い、SSMSで実行したSQL文をそのまま移行できる
                 * 途中で処理は入れられない
                 */
                /*string sql = @"
                    INSERT INTO employee(
                        nm_employee
                        ,kn_employee
                        ,mail_address
                        ,password
                        ,id_department
                    ) VALUES (
                        @name
                        ,@kana
                        ,@mail
                        ,@pass
                        ,@idDep
                    )
                ";*/

                // SQL方法②を使用
                sqlCommand = new SqlCommand(sb.ToString(), sqlCon, sqlTran);

                // @nameパラメータに設定
                SqlParameter param = sqlCommand.CreateParameter();
                param.ParameterName = "@name";
                param.SqlDbType = SqlDbType.NChar;
                param.Direction = ParameterDirection.Input;
                param.Value = "半田拓哉";
                sqlCommand.Parameters.Add(param);

                // @kanaパラメータに設定
                param = sqlCommand.CreateParameter();
                param.ParameterName = "@kana";
                param.SqlDbType = SqlDbType.NChar;
                param.Direction = ParameterDirection.Input;
                param.Value = "ハンダタクヤ";
                sqlCommand.Parameters.Add(param);

                // @mailパラメータに設定
                param = sqlCommand.CreateParameter();
                param.ParameterName = "@mail";
                param.SqlDbType = SqlDbType.NChar;
                param.Direction = ParameterDirection.Input;
                param.Value = "handa@hoge.jp";
                sqlCommand.Parameters.Add(param);

                // @passパラメータに設定
                param = sqlCommand.CreateParameter();
                param.ParameterName = "@pass";
                param.SqlDbType = SqlDbType.NChar;
                param.Direction = ParameterDirection.Input;
                param.Value = "aaa";
                sqlCommand.Parameters.Add(param);

                // @idDepパラメータに設定
                param = sqlCommand.CreateParameter();
                param.ParameterName = "@idDep";
                param.SqlDbType = SqlDbType.Int;    // id_departmentはint型なのでInt
                param.Direction = ParameterDirection.Input;
                param.Value = 10;
                sqlCommand.Parameters.Add(param);

                // SQL実行(戻り値は実行できた行数)
                insertRowCount = sqlCommand.ExecuteNonQuery();

                // トランザクションのコミット
                sqlTran.Commit();
            }
            finally
            {
                sqlCommand.Dispose();
                sqlCon.Close();
            }

            return insertRowCount;
        }
    }
}
