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
            #region RSA MOCK
            while (false)
            {
                //2 y 5 = 3 y 11
                //5 y 9
                Console.WriteLine("public key        " + RSA.getPublicKey(50, 96));
                /*byte[] a = (System.Text.Encoding.ASCII.GetBytes(Console.ReadLine()));
                byte[] b = (RSA.Encrypt(a));
                Console.WriteLine("byte encriptado   " + (System.Text.Encoding.UTF8.GetString(b)));
                Console.WriteLine("caracter decrypt  " + System.Text.Encoding.UTF8.GetString(RSA.Decrypt(b)));
                */
                int a = (int.Parse(Console.ReadLine()));
                int b = RSA.EncriptKey(a);
                Console.WriteLine("byte encriptado   " + b.ToString());
                Console.WriteLine("caracter decrypt  " + RSA.DecryptKey(b).ToString());
                Console.ReadKey();
            }
            #endregion 
            #region AES MOCK
            while (false)
            {
                string path = "C:\\Users\\sebas\\Desktop\\Test.txt";
                //UtilitiesForAES.getBlocks(FileOperations.getFileBytes(path)); // return List<List<byte>>
                UtilitiesForAES.getHexa(FileOperations.getFileBytes(path)); //return List<List<string>>
                AES.AES.Encrypt("Thats my Kung Fu");
            }
            #endregion
            //************************************************************************************

            while (!exit)
            {
                Console.Clear();
                menu();

            }
        }

        private static void menu()
        {
            Console.WriteLine("                                           Welcome to RSA and SDES ENCRYPTOR V 1.1");
            Console.WriteLine("                                                    -INSTRUCTIONS-");
            Console.WriteLine();
            Console.WriteLine("The first step is to select a encrypt method, type R for RSA or S for SDES.");
            Console.WriteLine("Once you have selected the method, follow these steps:");
            Console.WriteLine();
            Console.WriteLine("1) Encrypt: To encrypt a file follow these steps:");
            Console.WriteLine("1.1)Write in ENCRYPTOR level: -c -f (directory where the file is saved)");
            Console.WriteLine("1.2)Press Enter");
            Console.WriteLine("1.3)Enjoy the encryption.");
            Console.WriteLine("1.4)Search your file in the same directory with .cif at the end.");
            Console.WriteLine();
            Console.WriteLine("2) Deencrypt: To dencrypt a file follow these steps:");
            Console.WriteLine("2.1)Write in ENCRYPTOR level: -d -f (directory where the file is saved).comp");
            Console.WriteLine("2.2)Press Enter");
            Console.WriteLine("2.3)Search your file in the same directory with the original extension at the start.");
            Console.WriteLine();
            Console.Write("METHOD> ");
            string method = Console.ReadLine();
            if (method == "R" || method == "r")
            {
                Console.Write("ENCRYPTOR> ");
                CommandLine.isCorrectCommand("-c" + " " + "-f" + " " + Console.ReadLine(), "R");
                return;
            }
            if (method == "S" || method == "s")
            {
                Console.Write("ENCRYPTOR> ");
                CommandLine.isCorrectCommand("-c" + " " + "-f" + " " + Console.ReadLine(), "D");
                return;
            }
            Console.WriteLine("Incorrect method!");
            Console.ReadKey();
            Console.Clear();
            menu();
        }
    }
}
