using System;
using System.Collections.Generic;
using System.Text;

namespace ITword.Main
{
    public class INTcheck
    {
        public int INTchecker(int saidai)
        {
            string input1;
            int input2;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("こちらに数値を入力してください");
                Console.WriteLine($"※0～{saidai}のいずれかを半角数字入力");
                input1 = Console.ReadLine();
                try
                {
                    input2 = int.Parse(input1);
                    if (0 <= input2 && input2 <= saidai)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("指定された数値を入力してください");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("異常な値です");
                    continue;
                }
            }
            return input2;
        }
    }
}
