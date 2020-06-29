using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSystem_CUI.Dtos;
using EMSystem_CUI.Daos;
using EMSystem_CUI.Utils;

namespace EMSystem_CUI.Main
{
    class Program
    {
        /**
         * テーブル切り替え用変数 0：Employeeテーブル 1：Departmentテーブル
         * 追加課題用
         */
        static int tblFlg = 0;

        /// <summary>
        /// プログラム開始地点
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("システムを開始します");
            while (true)
            {
                try
                {
                    new Program().Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine("予期せぬエラーが発生しました");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        /// <summary>
        /// 処理選択を行う
        /// </summary>
        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine("現在のメンテナンス対象テーブル：" + TargetTableName(tblFlg));
            Console.WriteLine("1:検索 2:登録 3:更新 4:削除 9:テーブル切替 0:終了");
            string ope = Console.ReadLine();
            switch (ope)
            {
                case "1":
                    if (tblFlg == 0)
                    {
                        EmpSelect();
                    }
                    else
                    {
                        DepSelect();
                    }
                    break;
                case "2":
                    if (tblFlg == 0)
                    {
                        EmpInsert();
                    }
                    else
                    {
                        DepInsert();
                    }
                    break;
                case "3":
                    if (tblFlg == 0)
                    {
                        EmpUpdate();
                    }
                    else
                    {
                        DepUpdate();
                    }
                    break;
                case "4":
                    if (tblFlg == 0)
                    {
                        EmpDelete();
                    }
                    else
                    {
                        DepDelete();
                    }
                    break;
                case "9":
                    if (tblFlg == 0)
                    {
                        tblFlg = 1;
                    }
                    else
                    {
                        tblFlg = 0;
                    }
                    return;
                case "0":
                    Console.WriteLine("システムを終了します");
                    // システム終了の処理
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("指定の番号を入力してください");
                    break;
            }
        }

        /// <summary>
        /// 従業員検索
        /// </summary>
        public static void EmpSelect()
        {
            SqlConnection con = null;
            List<EmployeeDto> list = new List<EmployeeDto>();
            EmployeeDao empDao = new EmployeeDao(con);
            Console.WriteLine("1:全検索 2:条件検索 それ以外:戻る");
            string select = Console.ReadLine();
            try
            {
                con = DBUtil.GetConnection();
                switch (select)
                {
                    case "1":
                        list = empDao.SelectAll();
                        break;
                    case "2":
                        int validId = ValidateInt("ID");
                        list = empDao.SelectById(validId);
                        if (list.Count() == 0)
                        {
                            Console.WriteLine("入力された値で情報がみつかりませんでした");
                            break;
                        }
                        break;
                    default:
                        return;
                }
                DispSelectEmp(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                DBUtil.CloseConnection(con);
            }
        }

        /// <summary>
        /// 部署検索
        /// </summary>
        public static void DepSelect()
        {
            List<DepartmentDto> list = new List<DepartmentDto>();
            SqlConnection con = null;
            try
            {
                con = DBUtil.GetConnection();
                DepartmentDao depDao = new DepartmentDao(con);
                Console.WriteLine("1:全検索 2:条件検索 それ以外:戻る");
                string select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        list = depDao.SelectAll();
                        break;
                    case "2":
                        int validId = ValidateInt("ID");
                        list = depDao.SelectById(validId);
                        if (list.Count() == 0)
                        {
                            Console.WriteLine("入力された値で情報がみつかりませんでした");
                            break;
                        }
                        break;
                    default:
                        return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                DBUtil.CloseConnection(con);
            }
            DispSelectDep(list);
        }

        /// <summary>
        /// 従業員追加
        /// </summary>
        public static void EmpInsert()
        {
            SqlConnection con = null;
            SqlTransaction tran = null;
            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();
                EmployeeDao empDao = new EmployeeDao(con, tran);

                EmployeeDto dto = new EmployeeDto();
                dto.NmEmployee = InputParam("社員名");
                dto.KnEmployee = InputParam("フリガナ");
                dto.MailAddress = InputParam("メールアドレス");
                dto.Password = InputParam("パスワード");
                dto.IdDepartment = ValidateInt("部署ID");
                empDao.InsertEmployee(dto);
                tran.Commit();
                Console.WriteLine("登録が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }
 
        /// <summary>
        /// 部署登録
        /// </summary>
        public static void DepInsert()
        {
            SqlConnection con = null;
            SqlTransaction tran = null;
            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();
                DepartmentDao depDao = new DepartmentDao(con);

                DepartmentDto dto = new DepartmentDto();
                dto.IdDepartment = ValidateInt("部署ID");
                dto.NmDepartment = InputParam("部署名");
                depDao.InsertDepartment(dto);
                tran.Commit();
                Console.WriteLine("登録が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }

        /// <summary>
        /// 従業員更新
        /// </summary>
        public static void EmpUpdate()
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            SqlConnection con = null;
            SqlTransaction tran = null;

            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();
                EmployeeDao empDao = new EmployeeDao(con);

                int id = ValidateInt("ID");
                list = empDao.SelectById(id);
                if (list.Count == 0)
                {
                    Console.WriteLine("入力された値で情報がみつかりませんでした");
                    return;
                }
                EmployeeDto dto = new EmployeeDto();
                dto.NmEmployee = InputParam("社員名");
                dto.KnEmployee = InputParam("フリガナ");
                dto.MailAddress = InputParam("メールアドレス");
                dto.Password = InputParam("パスワード");
                dto.IdDepartment = ValidateInt("部署ID");
                empDao.UpdatetEmployee(id, dto);
                tran.Commit();
                Console.WriteLine("更新が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }

        /// <summary>
        /// 部署更新
        /// </summary>
        public static void DepUpdate()
        {
            List<DepartmentDto> list = new List<DepartmentDto>();
            SqlConnection con = null;
            SqlTransaction tran = null;

            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();
                DepartmentDao depDao = new DepartmentDao(con);

                int id = ValidateInt("ID");
                list = depDao.SelectById(id);
                if (list.Count == 0)
                {
                    Console.WriteLine("入力された値で情報がみつかりませんでした");
                    return;
                }
                DepartmentDto dto = new DepartmentDto();
                dto.IdDepartment = ValidateInt("部署ID");
                dto.NmDepartment = InputParam("部署名");
                depDao.UpdatetDepartment(id, dto);
                tran.Commit();
                Console.WriteLine("更新が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }

        /// <summary>
        /// 従業員削除
        /// </summary>
        public static void EmpDelete()
        {
            List<EmployeeDto> list = new List<EmployeeDto>();
            SqlConnection con = null;
            SqlTransaction tran = null;
            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();

                EmployeeDao empDao = new EmployeeDao(con);
                int validId = ValidateInt("ID");
                list = empDao.SelectById(validId);
                if (list.Count == 0)
                {
                    Console.WriteLine("入力された値で情報がみつかりませんでした");
                    return;
                }
                Console.WriteLine("指定されたIDの情報を削除します？ Y/y");
                Console.WriteLine("（Y/y以外の入力であればキャンセル）");
                string yY = Console.ReadLine();
                if (!("y" == yY) && !("Y" == yY))
                {
                    Console.WriteLine("削除をキャンセルしました");
                    return;
                }
                empDao.DeleteEmployee(validId);
                tran.Commit();
                Console.WriteLine("削除が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }

        /// <summary>
        /// 部署削除
        /// </summary>
        public static void DepDelete()
        {
            List<DepartmentDto> list = new List<DepartmentDto>();
            SqlConnection con = null;
            SqlTransaction tran = null;
            try
            {
                con = DBUtil.GetConnection();
                // トランザクション開始
                tran = con.BeginTransaction();
                DepartmentDao depDao = new DepartmentDao(con);
                int validId = ValidateInt("ID");
                list = depDao.SelectById(validId);
                if (list.Count == 0)
                {
                    Console.WriteLine("入力された値で情報がみつかりませんでした");
                    return;
                }
                Console.WriteLine("指定されたIDの情報を削除します？ Y/y");
                Console.WriteLine("（Y/y以外の入力であればキャンセル）");
                string yY = Console.ReadLine();
                if (!("y" == yY) && !("Y" == yY))
                {
                    Console.WriteLine("削除をキャンセルしました");
                    return;
                }
                depDao.DeleteDepartment(validId);
                tran.Commit();
                Console.WriteLine("削除が完了しました");
            }
            catch (Exception ex)
            {
                // ロールバック
                if (tran != null) { tran.Rollback(); }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (tran != null) { tran.Dispose(); }
                DBUtil.CloseConnection(con);
            }
        }


        /// <summary>
        /// フラグを判定し、テーブル名を取得する
        /// </summary>
        /// <param name="tblFlg">テーブル名判定用フラグ</param>
        /// <returns>テーブル名</returns>
        public static string TargetTableName(int tblFlg)
        {
            string rtnTblName = "";
            if (tblFlg == 0)
            {
                rtnTblName = "社員テーブル";
            }
            else
            {
                rtnTblName = "部署テーブル";
            }
            return rtnTblName;
        }

        /// <summary>
        /// 文字列の入力と入力値が空文字orNULLではないか判定する
        /// </summary>
        /// <param name="titleName">入力してもらう項目名</param>
        /// <returns>入力してもらった文字列</returns>
        public static string InputParam(string titleName)
        {
            string str = null;
            while (true)
            {
                Console.WriteLine($"{titleName}を入力してください");
                str = Console.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine($"{titleName}は入力必須です。");
                    continue;
                }

                break;
            }
            return str;
        }

        /// <summary>
        /// 数値型の入力とint型チェック処理
        /// </summary>
        /// <param name="titleName">入力してもらう項目名</param>
        /// <returns>int型に変換した入力値</returns>
        public static int ValidateInt(string titleName)
        {
            int inputNum = 0;
            while (true)
            {
                var str = InputParam(titleName);
                if (!(int.TryParse(str, out inputNum)))
                {
                    Console.WriteLine($"{titleName}は数値入力です");
                    continue;
                }
                break;
            }
            return inputNum;
        }

        /// <summary>
        /// 社員テーブル情報を表示する
        /// </summary>
        /// <param name="list">社員情報リスト</param>
        private static void DispSelectEmp(List<EmployeeDto> list)
        {
            foreach (EmployeeDto dto in list)
            {
                Console.WriteLine(dto.IdEmployee + "\t" + dto.NmEmployee + "\t" + dto.KnEmployee + "\t"
                        + dto.MailAddress + "\t" + dto.Password + "\t" + dto.IdDepartment);
            }
        }

        /// <summary>
        /// 部署テーブル情報を表示する
        /// </summary>
        /// <param name="list">部署情報リスト</param>
        private static void DispSelectDep(List<DepartmentDto> list)
        {
            foreach (DepartmentDto dto in list)
            {
                Console.WriteLine(dto.IdDepartment + "\t" + dto.NmDepartment);
            }
        }
    }
}
