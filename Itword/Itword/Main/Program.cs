using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ITword.Daos;

namespace ITword.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IT辞書データベースにようこそ");
            Console.WriteLine("以下のうちから操作を選択し入力してください");
            Console.WriteLine("0:終了");
            Console.WriteLine("1:検索");
            Console.WriteLine("2:入力");
            Console.WriteLine("3:削除");

            var check1 = new INTcheck();
            int case1 = check1.INTchecker(3);

            DataTable result = null;

            switch(case1)
            {
                case 0:
                    Console.WriteLine("プログラムを終了します");
                    Environment.Exit(1);
                    break;
                case 1:
                    result = BaseDao.GoSql();
                    break;
                case 2:
                    BaseDao.Insertsql();
                    break;
                case 3:


                    break;
            }
        }
    }
}
