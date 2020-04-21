using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CollisionMD5
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefix = "Paste your index in hex x2 format here";
            Console.WriteLine($"Searching for MD5 collision with prefix 0x{prefix} ...");
            using (var md5 = MD5.Create())
            {
                var encrypter = new Encrypter(md5);
                Stopwatch timer = new Stopwatch();
                timer.Start();
                var collision = Task.Run(() => encrypter.FindCollision(prefix));
                while(!collision.IsCompleted)
                {
                    Console.Write("Time elapsed: {0:hh\\:mm\\:ss}\r", timer.Elapsed);
                    System.Threading.Thread.Sleep(100);
                }
                Console.WriteLine("Collision found for m1: {0} and m2: {1}.", 
                    collision.Result.Item1, collision.Result.Item2);
                Console.ReadKey();
            }
        }
    }
}
