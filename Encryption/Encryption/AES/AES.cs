using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption.Utilities;
namespace Encryption.AES
{
    static class AES
    {
        /*
        *  Add round key
        *  Mix columns
        *  Shift rows
        *  Byte substitution
        */
        public static void Encrypt(string strkey)
        {
            string[][] KEYS = UtilitiesForAES.getKEY(strkey);
            KEYS = Roundkey(KEYS);
        }
        public static string[][][] GetData(string[][] DATA)
        {
            string[][][] Blocks = new string[DATA.Length][][];
            for (int i = 0; i < DATA.Length; i++)
            {
                Blocks[i] = DATA[i]
                    .Select((s, k) => new { Value = s, Index = k })
                    .GroupBy(x => x.Index / 4)
                    .Select(grp => grp.Select(x => x.Value).ToArray())
                    .ToArray();
            }
            return Blocks;
        }

        public static string[][] Roundkey(string[][] KEYA)
        {   //shift left
            int count = 0;
            string temp = KEYA[3][0];
            for (int i = 0; i < 3; i++)
            {
                KEYA[3][i] = KEYA[3][i + 1];
            }
            KEYA[3][3] = temp;
            //switch box
            List<string> SBox = UtilitiesForAES.GetSboxLst();
            for (int i = 1; i < 4; i++)
            {
                KEYA[3][i] = SBox.ElementAt(int.Parse(KEYA[3][i], System.Globalization.NumberStyles.HexNumber));
            }
            //(01, 00, 00, 00)
         // int a = SBox.IndexOf("B6");
            KEYA[3][0] = SBox.ElementAt(int.Parse(KEYA[3][0], System.Globalization.NumberStyles.HexNumber) + 10);
            string[][] KEYB = KEYA;// new string[4][];
            //w[4] = w[0] ⊕ g
            for (int i = 0; i < 4; i++)
            {
                KEYB[0][i] = UtilitiesForAES.exclusiveOR(KEYA[0][i], KEYA[3][i]);
            }
            //w[5] = w[4] ⊕ w[1]
            for (int i = 0; i < 4; i++)
            {
                KEYB[1][i] = UtilitiesForAES.exclusiveOR(KEYB[0][i], KEYA[0][i]);
            }
            //w[6] = w[5] ⊕ w[2] 
            for (int i = 0; i < 4; i++)
            {
                KEYB[2][i] = UtilitiesForAES.exclusiveOR(KEYB[1][i], KEYA[1][i]);
            }
            //w[7] = w[6] ⊕ w[3] 
            for (int i = 0; i < 4; i++)
            {
                KEYB[3][i] = UtilitiesForAES.exclusiveOR(KEYB[2][i], KEYA[3][i]);
            }

            return KEYB;
        }
    }
}
