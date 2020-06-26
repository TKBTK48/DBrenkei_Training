using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DBAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("検索モードの選択");
                Console.WriteLine("1:全検索 (Employeeテーブル)");
                Console.WriteLine("2:メールアドレス検索（インジェクション）");
                Console.WriteLine("3:メールアドレス検索（インジェクション対策）");
                Console.WriteLine("4:全検索 (departentテーブル)");
                Console.WriteLine("5:部署検索（インジェクション）");
                Console.WriteLine("6:部署検索（インジェクション対策）");
                Console.WriteLine("7:従業員追加（べた書きデータ）");
                Console.WriteLine("0:終了");
                Console.WriteLine();

                string input = Console.ReadLine();

                // 結果セット用のDataTable
                DataTable result = null;
                switch (input)
                {
                    case "0":
                        Console.WriteLine("プログラムを終了します");
                        Environment.Exit(1);
                        break;
                    case "1":
                        result = DBAccessEmployee.ExecuteSql();
                        break;

                    case "2":
                    case "3":
                        Console.WriteLine("検索したいメールアドレスを入力してください");
                        string mail = Console.ReadLine();

                        if (input == "2")
                        {
                            result = DBAccessEmployee.ExceuteSqlInjection(mail);
                        }
                        else
                        {
                            result = DBAccessEmployee.ExceuteSqlByMail(mail);
                        }
                        break;

                    case "4":
                        result = DBAccessDepartment.ExecuteSql();
                        break;

                    case "5":
                    case "6":
                        Console.WriteLine("検索したい部署名を入力してください。");
                        string department = Console.ReadLine();

                        if (input == "4")
                        {
                            result = DBAccessDepartment.ExceuteSqlInjection(department);
                        }
                        else
                        {
                            result = DBAccessDepartment.ExceuteSqlByDepartment(department);
                        }
                        break;
                    case "7":
                        Console.WriteLine("従業員を追加して、そのあと全件検索します。");
                        int insertRowCount = DBAccessEmployee.InsertEmployee();
                        Console.WriteLine($"{insertRowCount}人の従業員を追加しました。");

                        Console.WriteLine("全件検索して表示します。");
                        result = DBAccessEmployee.ExecuteSql();
                        break;

                    default:
                        Console.WriteLine("正しい数値を入力してください");
                        break;

                }

                // ロウはDataTable.Rowsで取得でき、型がDataRowCollection
                DataRowCollection rows = result.Rows;

                if (rows.Count > 0)
                {
                    // データがあったら検索結果の出力
                    Console.WriteLine("検索結果を出力");
                    // カラムはDataTable.Columnsで取得でき、型がDataColumnCollection
                    DataColumnCollection columns = result.Columns;

                    // カラム名表示
                    foreach (var column in columns)
                    {
                        Console.Write(column + "  ");
                    }

                    Console.WriteLine("\n--------------------------------------------------------------------------------");

                    // ロウの各データを表示
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
}