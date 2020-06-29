using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WesternCastle1
{
    public class Casenumbercheck
    {
        public string casenum1;
        public int case1;
        public void Casenumberchecker()
        {
            while (true)
            {
                casenum1 = Console.ReadLine();
                try
                {
                    case1 = int.Parse(casenum1);
                    if (case1 == 0 || case1 == 1 || case1 == 2 || case1 == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("不正な値です。再度入力してください");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("不正な値です。再度入力してください");
                    continue;
                }
            }
        }
        public string casenum2;
        public int case2;

        public void Casenumberchecker2()
        {
            while (true)
            {
                casenum2 = Console.ReadLine();
                try
                {
                    case2 = int.Parse(casenum2);
                    if (case2 == 0 || case2 == 1)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("不正な値です。再度入力してください");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("不正な値です。再度入力してください");
                    continue;
                }
            }
        }


        public string casenum3;
        public int case3;
        public void Casenumberchecker3()
        {
            while (true)
            {
                casenum3 = Console.ReadLine();
                try
                {
                    case3 = int.Parse(casenum3);
                    if (case3 == 0 || case3 == 1 || case3 == 2 || case3 == 3 || case3 == 4)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("不正な値です。再度入力してください");
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("不正な値です。再度入力してください");
                    continue;
                }
            }
        }



        public string casenum4;
        public int case4;
        public void Casenumberchecker4()
        {
            while (true)
            {
                casenum4 = Console.ReadLine();
                try
                {
                    case4 = int.Parse(casenum4);
                    break;

                }
                catch
                {
                    Console.WriteLine("不正な値です。再度入力してください");
                    continue;
                }
            }
        }
    }
}
