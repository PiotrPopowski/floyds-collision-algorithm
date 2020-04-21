using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CollisionMD5
{
    public class Encrypter
    {
        private readonly MD5 md5;
        StringBuilder sBuilder = new StringBuilder();

        public Encrypter(MD5 hashFunction)
        {
            md5 = hashFunction;
        }

        public Tuple<string, string> FindCollision(string prefix)
        {
            var result = FloydsCollisionAlgorithm(prefix);
            return result;
        }

        private Tuple<string, string> FloydsCollisionAlgorithm(string prefix)
        {
            var tortoise = hash(prefix);
            var hare = hash(prefix + hash(prefix));
            int counter = 0;
            while (tortoise != hare)
            {
                counter++;
                tortoise = hash(prefix + tortoise);
                hare = hash(prefix + hash(prefix + hare));
                if (counter % 10000000 == 0)
                    Console.WriteLine("Done {0} iterations.", counter);
            }

            Console.WriteLine("\rTortoise chased hare.");

            tortoise = "";
            var prevTortoise = tortoise;
            var prevHare = hare;
            tortoise = hash(prefix + tortoise);
            hare = hash(prefix + hare);
            while (tortoise != hare)
            {
                prevTortoise = tortoise;
                prevHare = hare;
                tortoise = hash(prefix + tortoise);
                hare = hash(prefix + hare);
            }

            return new Tuple<string, string>
                (prefix + prevTortoise, prefix + prevHare);
        }

        int _cut = 7;
        private string hash(string input)
        {
            var h = md5.ComputeHash(Hex.Parse(input));
            h = md5.ComputeHash(h);
            sBuilder.Clear();

            // Loop through the first 7 bytes of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < _cut; i++)
            {
                sBuilder.Append(h[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
