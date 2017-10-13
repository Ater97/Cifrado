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
        #region Ess

        public static int getKey(string OriginalExtenssion)
        {
            try
            {
                FileInfo myFile = new FileInfo(OriginalExtenssion + "C");
                myFile.Attributes &= ~FileAttributes.Hidden;
                string[] A = File.ReadAllLines(OriginalExtenssion + "C");
                var key = int.Parse(A[1]);
                myFile.Attributes |= FileAttributes.Hidden;
                return key;
            }
            catch
            {
                return 0;
            }
        }
        public static void CreateNewFileC(string completePath, int key) 
        {
            try
            {
                FileInfo file = new FileInfo(completePath);
                string path = file.Directory.ToString();
                string fileName = Path.GetFileNameWithoutExtension(completePath);
                //string Extension = Path.GetExtension(completePath);
                string Name = Path.GetFileName(completePath);
                string NewFileName = path + "\\" + fileName + ".cif";
                FileInfo myFile = new FileInfo(NewFileName + "C");
                if (File.Exists(NewFileName + "C"))
                {
                    myFile.Attributes &= ~FileAttributes.Hidden;
                }
                string[] dataToWrite = new string[2];
                dataToWrite[0] = Name;
                dataToWrite[1] = key.ToString();
                File.WriteAllLines(NewFileName+"C", dataToWrite);
                //File.WriteAllText(NewFileName + "C", Name);
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
                string[] A = File.ReadAllLines(OriginalExtenssion+"C");
                var extention = A[0];
                //string A = File.ReadLines(OriginalExtenssion + "C").First();
                myFile.Attributes |= FileAttributes.Hidden;
                var ext = string.Empty;
                for (int i = 0; i < extention.Length; i++)
                {
                    if (extention[i].Equals('.'))
                    {
                        for (int j = i; j < extention.Length; j++)
                        {
                            ext += extention[j];
                        }
                        return ext;
                    }
                }
                return A[0];
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
