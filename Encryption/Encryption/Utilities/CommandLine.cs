using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Utilities
{
    static class CommandLine
    {
        public static bool isCorrectCommand(string command, string method)
        {
            string[] commands = command.Split(' ');
            string[] newCommands = new string[3];
            string filePath = string.Empty;
            if (command == "") return false;
            if (commands.Length > 3)
            {
                for (int i = 2; i < commands.Length; i++)
                {
                    filePath = filePath + " " + commands[i];
                }
                newCommands[0] = commands[0];
                newCommands[1] = commands[1];
                newCommands[2] = filePath;
                return isEncrypting(newCommands, method);
            }
            if (commands.Length > 3) return false;
            return isEncrypting(commands, method);
            
        }

        private static bool fileExist(string command, string filePath, string method, string enOrDe)
        {
            if (command == "-f" || command == "-F")
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("ERROR: The file does not exists!");
                    Console.ReadKey();
                    return false;
                }
                //RSA Encrypt
                if (enOrDe == "c" && method == "R")
                {
                    //RSA Encrypt
                    byte[] encryptedData = RSA.Encrypt(FileOperations.getFileBytes(filePath));
                    FileOperations.writeEncryptedData(filePath, encryptedData);
                    Console.WriteLine("File encrypted successfully!");
                    Console.ReadKey();
                    return true; 
                }
                if (enOrDe == "d" && method == "R")
                {
                    //RSA Decrypt 
                    byte[] encryptedData = RSA.Encrypt(FileOperations.getFileBytes(filePath));
                    FileOperations.writeEncryptedData(filePath, encryptedData);
                    Console.WriteLine("File encrypted successfully!");
                    Console.ReadKey();
                    return true;
                }
                if (enOrDe == "c" && method == "D")
                {
                    //SDES Encrypt 
                    SDesProcess.startProcess(filePath);
                    Console.WriteLine("File encrypted successfully!");
                    Console.ReadKey();
                    return true;
                }
                if (enOrDe == "d" && method == "D")
                {
                    //SDES Decrypt 
                    byte[] encryptedData = RSA.Encrypt(FileOperations.getFileBytes(filePath));
                    FileOperations.writeEncryptedData(filePath, encryptedData);
                    Console.WriteLine("File encrypted successfully!");
                    Console.ReadKey();
                    return true;
                }
            }
            Console.WriteLine("ERROR -f command is missing!");
            Console.ReadKey();
            return false;
        }

        private static bool isEncrypting(string[] commands, string method)
        {
            if (commands[0] == "-c" || commands[0] == "-C") { 
                return fileExist(commands[1], commands[2], method, "c");
            }

            if (commands[0] == "-d" || commands[0] == "-D"){
                return fileExist(commands[1], commands[2], method,"d");
            }
            Console.WriteLine("ERROR -c or -d command is missing!");
            Console.ReadKey();
            return false;

        }
    }
}
