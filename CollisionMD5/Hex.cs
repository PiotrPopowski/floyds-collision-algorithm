using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionMD5
{
    public class Hex
    {
        public static byte[] Parse(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
        }

        public static string Parse(byte[] array)
        {
            string s="";
            foreach(byte b in array)
            {
                s += b.ToString("x2");
            }
            return s;
        }
    }
}
