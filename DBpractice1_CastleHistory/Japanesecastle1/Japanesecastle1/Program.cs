using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japanesecastle1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("日本の城データベースへようこそ");
            Console.WriteLine("操作したい内容を選択し、半角数字で入力してください");
            Console.WriteLine("0:終了");
            Console.WriteLine("1:検索");
            Console.WriteLine("2:入力");
            Console.WriteLine("3:削除");
            DataTable dataTable = null;
            var numbercheck1 = new NumberCheck();
            int choice1 = numbercheck1.NumberChecker(3);
            switch (choice1)
            {
                case 0:
                    Console.WriteLine("システムを終了します");
                    Environment.Exit(1);
                    break;


                case 1:
                    Console.WriteLine("操作したい内容を選択し、半角数字で入力してください");
                    Console.WriteLine("0:全件表示");
                    Console.WriteLine("1:条件指定");
                    var numbercheck2 = new NumberCheck();
                    int choice2 = numbercheck2.NumberChecker(1);
                    if (choice2 == 0)
                    {
                        dataTable = Search.AllSearch();
                    }
                    else
                    {
                        Console.WriteLine("操作したい内容を選択し、半角数字で入力してください");
                        Console.WriteLine("0:名称指定");
                        Console.WriteLine("1:築城年指定");
                        Console.WriteLine("2:所在都道府県指定");
                        Console.WriteLine("3:建築人物指定");
                        Console.WriteLine("4:重要度指定");
                        Console.WriteLine("5:防衛ランク指定");
                        Console.WriteLine("6:存在指定");
                        var numbercheck3 = new NumberCheck();
                        int choice3 = numbercheck3.NumberChecker(6);
                        if (choice3 == 0)
                        {
                            dataTable = Search.NameSearch();
                        }
                        else if (choice3==1)
                        {
                            dataTable = Search.BuildyearSearch();
                        }
                        else if (choice3 == 2)
                        {
                            dataTable = Search.PrefectureSearch();
                        }
                        else if (choice3 == 3)
                        {
                            dataTable = Search.OwnerSearch();
                        }
                        else if (choice3 == 4)
                        {
                            dataTable = Search.ImportantSearch();
                        }
                        else if (choice3 == 5)
                        {
                            dataTable = Search.DefenceSearch();
                        }
                        else if (choice3 == 6)
                        {
                            dataTable = Search.ImportantSearch();
                        }


                    }
                    break;


                case 2:
                    Console.WriteLine("新規データを入力します");
                    var insertor = new Insert();
                    int insertrow = insertor.InsertData();
                    Console.WriteLine($"{insertrow}件のデータが入力されました");
                    dataTable = Search.AllSearch();
                    break;



                case 3:
                    Console.WriteLine("データを削除します");
                    var deletor = new Delete();
                    insertrow = deletor.DeleteData();
                    Console.WriteLine($"{insertrow}件のデータが削除されました");
                    dataTable = Search.AllSearch();
                    break;

            }

            DataRowCollection rows = dataTable.Rows;





            if (rows.Count > 0)
            {
                Console.WriteLine("検索結果を出力");
                DataColumnCollection columns = dataTable.Columns;

                foreach (var column in columns)
                {
                    Console.Write(column + "\t");
                }
                Console.WriteLine("\n--------------------------------------------------------------------------------");

                for (int r = 0; r < rows.Count; r++)
                {
                    for (int c = 0; c < columns.Count; c++)
                    {
                        Console.Write(rows[r][c] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("検索結果は0件でした\n");
            }
        }
    }
}
