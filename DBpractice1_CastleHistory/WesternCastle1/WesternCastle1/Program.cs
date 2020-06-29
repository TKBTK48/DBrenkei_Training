using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WesternCastle1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("データベースへようこそ");
            Console.WriteLine("実行したい操作を以下から選び、半角数字を入力してください");
            Console.WriteLine("0:終了");
            Console.WriteLine("1:検索");
            Console.WriteLine("2:データ追加");
            Console.WriteLine("3:データ削除");
            Console.WriteLine();
            var casecheck1 = new Casenumbercheck();
            casecheck1.Casenumberchecker();
            int case1 = casecheck1.case1;
            string casenum1 = casecheck1.casenum1;

            DataTable output = null;
            switch (case1)
            {
                case 0:
                    Console.WriteLine("プログラムを終了します");
                    Environment.Exit(1);
                    break;
                case 1:
                    Console.WriteLine("検索の詳細選択に入ります");
                    Console.WriteLine("0:データ全出力");
                    Console.WriteLine("1:条件検索");
                    Console.WriteLine();
                    var casecheck2 = new Casenumbercheck();
                    casecheck2.Casenumberchecker2();
                    int case2 = casecheck2.case2;
                    string casenum2 = casecheck2.casenum2;
                    if (case2 == 0)
                    {
                        output = Search.ALLSearchsql();
                    }
                    //
                    else
                    {
                        Console.WriteLine("検索の条件選択に入ります");
                        Console.WriteLine("0:名称検索");
                        Console.WriteLine("1:築城年検索");
                        Console.WriteLine("2:国名検索");
                        Console.WriteLine("3:築城人名検索");
                        Console.WriteLine("4:重要度ランク検索");
                        Console.WriteLine();
                        var casecheck3 = new Casenumbercheck();
                        casecheck3.Casenumberchecker3();
                        int case3 = casecheck3.case3;
                        string casenum3 = casecheck3.casenum3;
                        //
                        if (case3 == 0)
                        {
                            Console.WriteLine("検索キーワードを入力してください");
                            string input = Console.ReadLine();
                            output = Search.NameChoiceSearchsql(input);
                        }
                        else if (case3 ==1)
                        {
                            Console.WriteLine("検索年号を入力してください");
                            var casecheck4 = new Casenumbercheck();
                            casecheck4.Casenumberchecker4();
                            int case4 = casecheck4.case4;
                            string casenum4 = casecheck4.casenum4;
                            int inputnum = case4;
                            output = Search.YearChoiceSearchsql(inputnum);
                        }
                        else if (case3 == 2)
                        {
                            Console.WriteLine("検索キーワードを入力してください");
                            string input = Console.ReadLine();
                            output = Search.CountryChoiceSearchsql(input);
                        }
                        else if (case3 == 3)
                        {
                            Console.WriteLine("検索キーワードを入力してください");
                            string input = Console.ReadLine();
                            output = Search.OwnerChoiceSearchsql(input);
                        }
                        else
                        {
                            Console.WriteLine("重要度を入力してください");
                            var casecheck4 = new Casenumbercheck();
                            casecheck4.Casenumberchecker4();
                            int case4 = casecheck4.case4;
                            string casenum4 = casecheck4.casenum4;
                            int inputnum = case4;
                            output = Search.ImportantChoiceSearchsql(inputnum);
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("データの追加を行います");
                    break;
                case 3:
                    Console.WriteLine("データの削除を行います");
                    break;
            }
            DataRowCollection rows = output.Rows;

            //if (rows.Count > 0)
            //{
            //    // データがあったら検索結果の出力
            //    Console.WriteLine("検索結果を出力");
            //    // カラムはDataTable.Columnsで取得でき、型がDataColumnCollection
            //    DataColumnCollection columns = output.Columns;

            //    // カラム名表示
            //    foreach (var column in columns)
            //    {
            //        Console.Write(column + "  ");
            //    }

            //    Console.WriteLine("\n--------------------------------------------------------------------------------");

            //    // ロウの各データを表示
            //    for (int r = 0; r < rows.Count; r++)
            //    {
            //        for (int c = 0; c < columns.Count; c++)
            //        {
            //            Console.Write(rows[r][c] + "\t");
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine("--------------------------------------------------------------------------------\n");
            //}
            //else
            //{
            //    Console.WriteLine("検索結果は0件でした\n");
            //}
        }
    }
}
