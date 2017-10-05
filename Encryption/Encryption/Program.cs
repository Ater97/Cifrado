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
            RCA rca = new RCA();
            int np = int.Parse(Console.ReadLine());
            Console.WriteLine(rca.PublicKey(np, int.Parse(Console.ReadLine())));


         
            Console.ReadKey();
        }
    }
}
