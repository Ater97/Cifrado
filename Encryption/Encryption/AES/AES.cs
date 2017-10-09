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
        public static string[][][] GetData(string [][]DATA)
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
        {
            int count = 0;

            string temp = KEYA[3][0];
            for (int i = 0; i < 3; i++)
            {
                KEYA[3][i] = KEYA[3][i + 1];   
            }
            KEYA[3][3] = temp;

            return KEYA;
        }
    }
}
