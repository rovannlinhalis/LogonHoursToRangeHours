using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExampleLogonHours
{
    public static class Funcoes
    {
        public static byte[] FromBinaryString(string binaryString)
        {
            binaryString = binaryString.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            var byteArray = Enumerable.Range(0, int.MaxValue / 8)
                                      .Select(i => i * 8)    // get the starting index of which char segment
                                      .TakeWhile(i => i < binaryString.Length)
                                      .Select(i => binaryString.Substring(i, 8)) // get the binary string segments
                                      .Select(s => Convert.ToByte(s, 2)) // convert to byte
                                      .ToArray();
            return byteArray;
        }

        public static string BinaryStringFromByteArray(byte[] data)
        {
            return string.Join("", data.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
        }
    }
}
