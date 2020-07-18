using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itword.Main
{
    public class Output
    {
        public void Outputer(DataTable result)
        {
            DataRowCollection row = result.Rows;
            if (row.Count > 0)
            {
                Console.WriteLine("結果を出力します");
                DataColumnCollection columns = result.Columns;

                Console.WriteLine("ID  ワード　関連システム１　関連システム２　関連システム３　詳細　更新日時");
                //foreach (var column in columns)
                //{
                //    Console.Write(columns + " ");
                //}
                Console.WriteLine("\n--------------------------------------------------------------------------------");

                for (int r = 0; r < row.Count; r++)
                {
                    for (int c = 0; c < columns.Count; c++)
                    {
                        Console.Write(row[r][c] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("検索結果がありません");
            }

        }
    }
}

