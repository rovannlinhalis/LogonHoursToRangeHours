using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonHoursToRangeHours
{
    class Program
    {
        static void Main(string[] args)
        {

            var binaryStr =
@"
11111111 11111111 11111111
11100000 00000000 00001111
11111111 11111100 00000000
00000000 00000000 11111111
11100000 00000000 00000111
11111111 11111000 00000000
00100000 00000000 11111111
";
            binaryStr = binaryStr.Replace(" ", "").Replace("\r\n", "");
            var byteArray = Enumerable.Range(0, int.MaxValue / 8)
                                      .Select(i => i * 8)    // get the starting index of which char segment
                                      .TakeWhile(i => i < binaryStr.Length)
                                      .Select(i => binaryStr.Substring(i, 8)) // get the binary string segments
                                      .Select(s => Convert.ToByte(s, 2)) // convert to byte
                                      .ToArray();

            string binfinal = string.Join("",byteArray.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

            
            int l = 3*8;
            Console.Write("    ");
            for (int h = 0; h < 24; h++)
                Console.Write(h.ToString("00") + " ");
            Console.WriteLine("");

            for (int d =0; d < 7; d++)
            {
                string dia = binfinal.Substring(l * d, l);
                Console.Write("D:" + d+ " ");

                foreach (var c in dia)
                {
                    Console.Write(" "+ c + " ");
                }

                Console.Write("Range: "+ dia.IndexOf('1').ToString("00")+" às "+ dia.LastIndexOf('1').ToString("00"));

                Console.WriteLine("");
            }



            Console.ReadKey();


        }
    }
}
