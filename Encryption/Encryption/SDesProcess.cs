using Encryption.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class SDesProcess
    {
        public static void startProcess(string filePath)
        {
            Console.WriteLine("Enter Key:");
            int key = 0;
            try
            {
                key = int.Parse(s: Console.ReadLine());
                key = RSA.EncriptKEYint(key);
            }
            catch (FormatException)
            {
                Console.WriteLine("Parse error!");
                startProcess(filePath);
            }

            SDesAlgorithm sDes = new SDesAlgorithm();

            IList<byte> k1;
            IList<byte> k2;
            if (!GenerateKeys(key, sDes, out k1, out k2))
            {
                Console.WriteLine("The key should be in the range: 0 < key < 1023.");
                startProcess(filePath);
            }
            Console.WriteLine("");
            
            string[] datos = File.ReadAllLines(filePath);
            string[] newDatos = new string[datos.Length];
            List<byte> dataToWrite = new List<byte>();
            for (int i = 0; i < datos.Length; i++)
            {
                var o = EncriptText(sDes, datos[i], k1, k2);
                newDatos[i] = o;
            }
            string newPath = FileOperations.CreateNewFileForSDes(filePath);
            File.WriteAllLines(newPath,newDatos);

            #region Ghost
            FileOperations.CreateNewFileC(filePath);
            #endregion
        }

        public static void DecryptAllData(string filePath)
        {
            FileOperations.getExtsC(filePath); // **return string original path**
            Console.WriteLine("Enter your personal key");
            int Key = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the key provided by the system");
            int privateKey = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the public key");
            int N = int.Parse(Console.ReadLine());
            RSA.DecriptKEYint(Key, privateKey, N);
            /*
            string[] data = File.ReadAllLines(newPath);
            string[] dataDecrypted = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                var o = DecriptText(sDes, data[i], k1, k2);
                dataDecrypted[i] = o;
            }
            File.WriteAllLines(filePath, dataDecrypted);*/
        }

        private static bool GenerateKeys(int key, SDesAlgorithm sDes, out IList<byte> k1, out IList<byte> k2)
        {
            sDes.GenerateKeys(key);
            var bytes = sDes.DecToBytes(key);
            k1 = null;
            k2 = null;
            if (bytes.Count != 0)
            {
                bytes = sDes.ToP10(bytes);
                Print("P10: {0}", bytes);

                bytes = sDes.CyclicShift_One(bytes);
                Print("Cyclin shift 1: {0}", bytes);

                k1 = sDes.ToP8(bytes);
                Print("K1: {0}", k1);

                bytes = sDes.CyclicShift_Two(bytes);
                Print("Cyclin shift {0}", bytes);

                k2 = sDes.ToP8(bytes);
                Print("K2: {0}", k2);
                return true;
            }
            return false;
        }

        public static List<byte> getList(byte [] bytes)
        {
            SDesAlgorithm sdes = new SDesAlgorithm();
            return sdes.Encript(bytes);
        }

        private static string EncriptText(SDesAlgorithm sDes, string text, IList<byte> k1, IList<byte> k2)
        {
            var str = string.Empty;
            for (var i = 0; i < text.Count(); i++)
            {
                var o = sDes.Encript(text[i].ToString());
                str += o;
            }
            return str;
        }

        
        private static string DecriptText(SDesAlgorithm sDes, string text, IList<byte> k1, IList<byte> k2)
        {
            var str = string.Empty;
            for (var i = 0; i < text.Length; i++)
            {
                var o = sDes.Decript(text[i].ToString());
                str += o;
            }
            return str;
        }

        private static void Print(string text, IList<byte> bytes)
        {
            Console.WriteLine(text, string.Join("", bytes));
        }
    }
}
