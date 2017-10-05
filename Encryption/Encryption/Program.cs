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
            Utilities U = new Utilities();


            //Console.WriteLine(U.GetPrimeNumber(Convert.ToInt32(Console.ReadLine())));
            int a =int.Parse(Console.ReadLine());
            Console.WriteLine(U.Coprime(a, int.Parse(Console.ReadLine())));

            Console.ReadKey();
        }
    }
}
