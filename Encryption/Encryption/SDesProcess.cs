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

        public static void EncriptProcess(string filePath)
        {
            var dataToEncript = FileOperations.getFileBytes(filePath);
            var key = 62;
            SDesAlgorithm sDes = new SDesAlgorithm();
            sDes.GenerateKeys(key);
            IList<byte> k1 = sDes.getK1();
            IList<byte> k2 = sDes.getK2();
            var dataEncripted = new byte[dataToEncript.Length];
            var bytesToSend = new byte[8];
            for (int i = 0; i < dataToEncript.Length; i+=8)
            {
                if (i+8 > dataToEncript.Length)
                {
                    var newBytesToSend = new byte[8];
                    for (int j = 0; j < dataEncripted.Length - i; j++)
                    {
                        newBytesToSend[j] = dataToEncript[dataEncripted.Length - i + j];
                    }
                    var encryptedBytes = EncriptBytes(sDes, newBytesToSend, k1, k2);
                    for (int k = 0; k < newBytesToSend.Length; k++)
                    {
                        dataEncripted[dataToEncript.Length - i + k] = encryptedBytes[k];
                    }
                    i = dataEncripted.Length+1;
                }
                else
                {
                    bytesToSend[0] = dataToEncript[i];
                    bytesToSend[1] = dataToEncript[i + 1];
                    bytesToSend[2] = dataToEncript[i + 2];
                    bytesToSend[3] = dataToEncript[i + 3];
                    bytesToSend[4] = dataToEncript[i + 4];
                    bytesToSend[5] = dataToEncript[i + 5];
                    bytesToSend[6] = dataToEncript[i + 6];
                    bytesToSend[7] = dataToEncript[i + 7];
                    var encryptedbytes = EncriptBytes(sDes,bytesToSend,k1,k2);
                    dataEncripted[i] = encryptedbytes[0];
                    dataEncripted[i+1] = encryptedbytes[1];
                    dataEncripted[i+2] = encryptedbytes[2];
                    dataEncripted[i+3] = encryptedbytes[3];
                    dataEncripted[i+4] = encryptedbytes[4];
                    dataEncripted[i+5] = encryptedbytes[5];
                    dataEncripted[i+6] = encryptedbytes[6];
                    dataEncripted[i+7] = encryptedbytes[7];
                }
            }
            FileOperations.writeEncryptedData(filePath,dataEncripted);
        }

        public static void DecriptProcess(string filePath)
        {
            var dataToDecript = FileOperations.getFileBytes(filePath);
            var key = 62;
            SDesAlgorithm sDes = new SDesAlgorithm();
            sDes.GenerateKeys(key);
            IList<byte> k1 = sDes.getK1();
            IList<byte> k2 = sDes.getK2();
            var dataDecripted = new byte[dataToDecript.Length];
            var bytesToSend = new byte[8];
            for (int i = 0; i < dataToDecript.Length; i += 8)
            {
                if (i + 8 > dataToDecript.Length)
                {
                    var newBytesToSend = new byte[8];
                    for (int j = 0; j < dataDecripted.Length - i; j++)
                    {
                        newBytesToSend[j] = dataToDecript[dataDecripted.Length - i + j];
                    }
                    var encryptedBytes = DecriptBytes(sDes, newBytesToSend, k1, k2);
                    for (int k = 0; k < newBytesToSend.Length; k++)
                    {
                        dataDecripted[dataToDecript.Length - i + k] = encryptedBytes[k];
                    }
                    i = dataDecripted.Length + 1;
                }
                else
                {
                    bytesToSend[0] = dataToDecript[i];
                    bytesToSend[1] = dataToDecript[i + 1];
                    bytesToSend[2] = dataToDecript[i + 2];
                    bytesToSend[3] = dataToDecript[i + 3];
                    bytesToSend[4] = dataToDecript[i + 4];
                    bytesToSend[5] = dataToDecript[i + 5];
                    bytesToSend[6] = dataToDecript[i + 6];
                    bytesToSend[7] = dataToDecript[i + 7];
                    var encryptedbytes = DecriptBytes(sDes, bytesToSend, k1, k2);
                    dataDecripted[i] = encryptedbytes[0];
                    dataDecripted[i + 1] = encryptedbytes[1];
                    dataDecripted[i + 2] = encryptedbytes[2];
                    dataDecripted[i + 3] = encryptedbytes[3];
                    dataDecripted[i + 4] = encryptedbytes[4];
                    dataDecripted[i + 5] = encryptedbytes[5];
                    dataDecripted[i + 6] = encryptedbytes[6];
                    dataDecripted[i + 7] = encryptedbytes[7];
                }
            }
            var ext = FileOperations.getExtsC(filePath);
            FileOperations.CreateNewFile(filePath,ext, dataDecripted);
        }
        public static void startProcess(string filePath)
        {
            Console.WriteLine("Enter Key:");
            int key = 0;
            try
            {
                byte[] array = Encoding.ASCII.GetBytes(Console.ReadLine());
                for (int i = 0; i < array.Count(); i++)
                {
                    key += array[i];
                }
                key = RSA.EncriptKEYint(key, array[array.Count()-1]);
            }
            catch (FormatException)
            {
                Console.WriteLine("Parse error!");
                startProcess(filePath);
            }

            SDesAlgorithm sDes = new SDesAlgorithm();
            sDes.GenerateKeys(key);
            IList<byte> k1 = sDes.getK1();
            IList<byte> k2 = sDes.getK2();
            
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
            int Key = 0;
            Console.WriteLine("Enter your personal key");
            byte[] array = Encoding.ASCII.GetBytes(Console.ReadLine());
            for (int i = 0; i < array.Count(); i++)
            {
                Key += array[i];
            }
            Console.WriteLine("Enter the key provided by the system");
            int privateKey = int.Parse(Console.ReadLine());
            Key = RSA.EncriptKEYint(Key, array[array.Count() - 1]);
            SDesAlgorithm sDes = new SDesAlgorithm();
            sDes.GenerateKeys(Key);
            var ext = FileOperations.getExtsC(filePath); // **return string original path**

            IList<byte> k1 = sDes.getK1();
            IList<byte> k2 = sDes.getK2();

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
        private static bool GenerateKeys(int key, SDesAlgorithm sDes, out IList<byte> k1, out IList<byte> k2)
        {
            sDes.GenerateKeys(key);
            var bytes = sDes.DecToBytes(key);
            k1 = null;
            k2 = null;
            if (bytes.Count != 0)
            {
                k1 = sDes.ToP8(bytes);
                k2 = sDes.ToP8(bytes);
                return true;
            }
            return false;
        }
        
        private static byte [] EncriptBytes(SDesAlgorithm sDes, byte [] data, IList<byte> k1, IList<byte> k2)
        {
            var str = new byte[8];
            for (int i = 0; i < data.Length; i++)
            {
                var encryptedByte = sDes.Encript(data[i]);
                str[i] = encryptedByte;
            }
            return str;
        }

        private static byte[] DecriptBytes(SDesAlgorithm sDes, byte[] data, IList<byte> k1, IList<byte> k2)
        {
            var str = new byte[8];
            for (int i = 0; i < data.Length; i++)
            {
                var encryptedByte = sDes.Decript(data[i]);
                str[i] = encryptedByte;
            }
            return str;
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
        
    }
}
