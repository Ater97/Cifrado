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
            var bytes = lecturaBinaria.ReadBytes((int)original.Length);
            lecturaBinaria.Close();
            return bytes;
        }

    }
}
