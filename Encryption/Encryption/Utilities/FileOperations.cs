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
            fs.Close();
            return true;
        }

        public static bool CreateNewFile(string filePath,string ext, byte[] tempByte)
        {
            FileInfo file = new FileInfo(filePath);
            string path = file.Directory.ToString();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string NewFileName = path + "\\" + fileName + ext;
            FileInfo myFile = new FileInfo(NewFileName);
            FileStream fs = new FileStream(NewFileName, FileMode.Create, FileAccess.Write);
            fs.Write(tempByte, 0, tempByte.Count());
            fs.Flush();
            fs.Close();
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
        public static string CreateNewFileForSDesDencryption(string filePath, string extention)
        {
            FileInfo file = new FileInfo(filePath);
            string path = file.Directory.ToString();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string NewFileName = path + "\\" + fileName + extention;
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
        public static void CreateNewFileC(string completePath) 
        {
            try
            {
                FileInfo file = new FileInfo(completePath);
                string path = file.Directory.ToString();
                string fileName = Path.GetFileNameWithoutExtension(completePath);
                string extension = Path.GetExtension(completePath);
                string Name = Path.GetFileName(completePath);
                string NewFileName = path + "\\" + fileName + ".cif";
                FileInfo myFile = new FileInfo(NewFileName + "C");
                if (File.Exists(NewFileName + "C"))
                {
                    myFile.Attributes &= ~FileAttributes.Hidden;
                }
               
                File.WriteAllText(NewFileName + "C", extension);
                myFile.Attributes |= FileAttributes.Hidden;
            }
            catch
            {

            }
        }
        public static string getExtsC(string OriginalExtenssion) 
        {
            try
            {
                FileInfo myFile = new FileInfo(OriginalExtenssion + "C");
                myFile.Attributes &= ~FileAttributes.Hidden;
                string A = File.ReadLines(OriginalExtenssion + "C").First();
                myFile.Attributes |= FileAttributes.Hidden;
                return A; 
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
