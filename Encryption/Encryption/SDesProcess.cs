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

        public static void ShowAllKeys()
        {
            SDesAlgorithm sDes = new SDesAlgorithm();
            IList<byte> k1;
            IList<byte> k2;
            GenerateKeysGhost(555, sDes, out k1, out k2);
            /*  Console.ForegroundColor = ConsoleColor.Green;
              Console.WriteLine("K1: " + String.Join(string.Empty,k1));
              Console.WriteLine("K2: " + String.Join(string.Empty, k2));
              Console.ForegroundColor = ConsoleColor.White;*/
        }

        public static void startProcess(string filePath)
        {
            //RSA
            Console.WriteLine("Type your password:");
            byte[] arrapas = Encoding.ASCII.GetBytes(Console.ReadLine());
            var key = 0;
            for (int i = 0; i < arrapas.Count(); i++)
            {
                key += arrapas[i];
            }
            key = RSA.EncriptKEYint(key, 555);

            SDesAlgorithm sDes = new SDesAlgorithm();

            IList<byte> k1;
            IList<byte> k2;

            GenerateKeys(555, sDes, out k1, out k2);
            string[] datos = File.ReadAllLines(filePath);
            string[] newDatos = new string[datos.Length];
            List<byte> dataToWrite = new List<byte>();
            for (int i = 0; i < datos.Length; i++)
            {
                var o = EncriptText(sDes, datos[i], k1, k2);
                newDatos[i] = o;
            }
            string newPath = FileOperations.CreateNewFileForSDes(filePath);
            File.WriteAllLines(newPath, newDatos);

            FileOperations.CreateNewFileC(filePath, key);
        }

        public static void DecryptAllData(string filePath)
        {
            SDesAlgorithm sDes = new SDesAlgorithm();
            var ext = FileOperations.getExtsC(filePath); // **return string original path**
            //RSA
            Console.WriteLine("Type your password:");
            byte[] arrapas = Encoding.ASCII.GetBytes(Console.ReadLine());

            var userkey = 0;
            for (int i = 0; i < arrapas.Count(); i++)
            {
                userkey += arrapas[i];
            }
            var key = RSA.DecriptKEYint(userkey, FileOperations.getKey(filePath));

            IList<byte> k1;
            IList<byte> k2;
            GenerateKeysGhost(key, sDes, out k1, out k2);

            string[] data = File.ReadAllLines(filePath);
            string newFileName = FileOperations.CreateNewFileForSDesDencryption(filePath, ext);
            string[] dataDecrypted = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                var o = DecriptText(sDes, data[i], k1, k2);
                dataDecrypted[i] = o;
            }
            File.WriteAllLines(newFileName, dataDecrypted);
        }

        private static bool GenerateKeysGhost(int key, SDesAlgorithm sDes, out IList<byte> k1, out IList<byte> k2)
        {
            sDes.GenerateKeys(key);
            var bytes = sDes.DecToBytes(key);
            k1 = null;
            k2 = null;
            if (bytes.Count != 0)
            {
                bytes = sDes.ToP10(bytes);

                bytes = sDes.CyclicShift_One(bytes);

                k1 = sDes.ToP8(bytes);

                bytes = sDes.CyclicShift_Two(bytes);

                k2 = sDes.ToP8(bytes);
                return true;
            }
            return false;
        }

        private static bool GenerateKeys(int key, SDesAlgorithm sDes, out IList<byte> k1, out IList<byte> k2)
        {
            sDes.GenerateKeys(key);
            var bytes = sDes.DecToBytes(key);
            k1 = null;
            k2 = null;
            if (bytes.Count != 0)
            {
                Console.WriteLine(Environment.NewLine + "Inf.");
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

        public static List<byte> getList(byte[] bytes)
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
            sDes.k1 = k1;
            sDes.k2 = k2;
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
