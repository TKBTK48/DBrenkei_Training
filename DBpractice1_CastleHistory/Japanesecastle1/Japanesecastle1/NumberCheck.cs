using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japanesecastle1
{
    public class NumberCheck
    {
        public int num;
        public int NumberChecker(int saidaichoice)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("こちらに数値を入力してください");
                try
                {
                    num = int.Parse(Console.ReadLine());
                    if (0 <= num && num <= saidaichoice)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("入力した値が異常です");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("入力した値が異常です");
                    continue;
                }
            }
            return num;

        }
    }
}
