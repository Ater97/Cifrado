using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Utilities
{
    static class FileOperations
    {
        public static byte[] getFileBytes(string filePath) {
            FileStream original = new FileStream(filePath, FileMode.Open);
            BinaryReader lecturaBinaria = new BinaryReader(original);
            byte [] bytes = lecturaBinaria.ReadBytes((int)original.Length);
            lecturaBinaria.Close();
            return bytes;
        }

        private static bool CreateNewFile(string filePath, byte[] tempByte)
        {
            FileInfo file = new FileInfo(filePath);
            string path = file.Directory.ToString();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string NewFileName = path + "\\" + fileName + ".cif";
            FileInfo myFile = new FileInfo(NewFileName);
            FileStream fs = new FileStream(NewFileName, FileMode.Create, FileAccess.Write);
            fs.Write(tempByte, 0, tempByte.Count());
            fs.Flush();
            return true;
        }

        public static string CreateNewFileForSDes(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            string path = file.Directory.ToString();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string NewFileName = path + "\\" + fileName + ".cif";
            if (!File.Exists(filePath))
            {
                File.Create(NewFileName);
            }
            return NewFileName;
        }

        public static bool writeEncryptedData(string filePath,byte [] dataEncrypted)
        {
            try
            {
                CreateNewFile(filePath,dataEncrypted);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region Essential
        private static void CreateNewFileC(string completePath, byte[] tempByte) 
        {
            FileInfo file = new FileInfo(completePath);
            string path = file.Directory.ToString();
            string fileName = Path.GetFileNameWithoutExtension(completePath);
            string NewFileName = path + "\\" + fileName + ".ENC";
            FileInfo myFile = new FileInfo(NewFileName + "C");
            if (File.Exists(NewFileName +"C" ))
            {
                myFile.Attributes &= ~FileAttributes.Hidden;
            }
            File.WriteAllText(NewFileName + "C", path);
            myFile.Attributes |= FileAttributes.Hidden;
            FileStream fs = new FileStream(NewFileName, FileMode.Create, FileAccess.Write);
            fs.Write(tempByte, 0, tempByte.Count());
            fs.Flush();
        }
        static string OriginalExtenssion;
        static string A;
        private static void getExtsC(string path) 
        {
            FileInfo myFile = new FileInfo(OriginalExtenssion + ".ENCC");
            myFile.Attributes &= ~FileAttributes.Hidden;
            A = File.ReadLines(OriginalExtenssion + ".ENCC").ToString();
            myFile.Attributes |= FileAttributes.Hidden;
        }

        #endregion

    }
}
