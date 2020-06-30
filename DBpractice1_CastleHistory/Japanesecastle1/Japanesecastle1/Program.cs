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

                    }
                    break;


                case 2:

                    break;



                case 3:
                    break;

            }


        }
    }
}
