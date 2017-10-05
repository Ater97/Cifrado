using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA rca = new RSA();
            int np = int.Parse(Console.ReadLine());
            Console.WriteLine(rca.getPublicKey(np, int.Parse(Console.ReadLine())));
         
            Console.ReadKey();
        }
    }
}
