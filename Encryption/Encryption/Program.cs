using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption.Utilities;
using Encryption.AES;

namespace Encryption
{
    class Program
    {
        static bool exit = false;
        static void Main(string[] args)
        {
            while (!exit)
            {
                Console.Clear();
                menu();

            }
        }

        private static void menu()
        {
            Console.WriteLine("                                             Welcome to SDES ENCRYPTOR V 1.1");
            Console.WriteLine("                                                    -INSTRUCTIONS-");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1) Encrypt: To encrypt a file follow these steps:");
            Console.WriteLine("1.1)Write in ENCRYPTOR level: -c -f (directory where the file is saved)");
            Console.WriteLine("1.2)Press Enter");
            Console.WriteLine("1.3)Enjoy the encryption.");
            Console.WriteLine("1.4)Search your file in the same directory with .cif at the end.");
            Console.WriteLine();
            Console.WriteLine("2) Deencrypt: To dencrypt a file follow these steps:");
            Console.WriteLine("2.1)Write in ENCRYPTOR level: -d -f (directory where the file is saved).cif");
            Console.WriteLine("2.2)Press Enter");
            Console.WriteLine("2.3)Search your file in the same directory with the original extension at the start.");
            Console.WriteLine();
            Console.Write("ENCRYPTOR> ");
            CommandLine.isCorrectCommand(Console.ReadLine(), "D");
            Console.ReadKey();
            Console.Clear();
            menu();
        }
    }
}
