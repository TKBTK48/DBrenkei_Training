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
            Console.WriteLine();
            Console.WriteLine($"現在のメンテナンス対象テーブル：{TargetTableName(tblFlg)}");
            int choice;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1:検索 2:登録 3:更新 4:削除 9:テーブル切替 0:終了");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("値が不正です");
                    continue;
                }
            }
            if (choice == 1)
            {
                BaseDao<string> list = new BaseDao<string>();
            }
            else if (choice == 2)
            {
                string name=InputParam("社員名");
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
        private static string InputParam(string titleName)
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
        private static int ValidateInt(string titleName)
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
