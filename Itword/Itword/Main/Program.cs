using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ITword.Daos;
using Itword.Main;

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
            Console.WriteLine("3:更新");
            Console.WriteLine("4:削除");
            Console.WriteLine("5:学習モード");

            var check1 = new INTcheck();
            int case1 = check1.INTchecker(5);

            DataTable result = null;

            switch(case1)
            {
                case 0:
                    Console.WriteLine("プログラムを終了します");
                    Environment.Exit(1);
                    break;
                case 1:
                    Console.WriteLine("検索条件を選択して入力してください");
                    Console.WriteLine("0:全検索");
                    Console.WriteLine("1:条件検索");
                    var check2 = new INTcheck();
                    int case2 = check2.INTchecker(1);
                    if (case2 == 0)
                    {
                        result = BaseDao.GoSql();
                        var output2 = new Output();
                        output2.Outputer(result);

                        break;
                    }
                    else
                    {
                        result = BaseDao.Filtering();
                        var output2 = new Output();
                        output2.Outputer(result);
                        break;
                    }

                case 2:
                    BaseDao.Insertsql();
                    break;
                case 3:
                    BaseDao.Update();
                    break;
                case 4:
                    BaseDao.Deletesql();

                    break;
                case 5:
                    int row = BaseDao.Max();
                    BaseDao.Learnsql(row);
                    break;
            }
        }
    }
}
