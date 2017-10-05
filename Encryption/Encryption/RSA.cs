using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    static class RSA
    {
        static int PrivateKey = 0;
        static int[] PublicKey = new int[2];
        

        public static string getPublicKey(int np, int nq) // n prime number
        {
            int p = UtilitiesForRSA.GetPrimeNumber(np);
            int q = UtilitiesForRSA.GetPrimeNumber(nq);
            int[] NyZ = UtilitiesForRSA.Phi(p, q);
            int k = UtilitiesForRSA.getFirstCoprime(NyZ[1], 50, 100);
            PrivateKey = getPrivateKEy(k, NyZ[1], 100);
            PublicKey[0] = NyZ[0];
            PublicKey[1] = k;
            return NyZ[0].ToString() + "," + k.ToString();
        }
        
        public static int getPrivateKEy(int k, int z, int length)
        {    //k* j = 1(mod z) Private key
            for (int i = 0; i < length; i++)
            {
                if (((k * i) % z) == 1)
                    return i;
            }
            return 0;
        }

        private static byte Encrypt(byte E)
        { //P^k = E ( mod n )
            int P = (Int32)(E);
            double P2 = (Math.Pow(P, PublicKey[1]));
            P2 = P2 % PublicKey[0];
            P = (int) Math.Round(P2);
            return (byte)P;
        }

        public static byte[] Encrypt(byte[] E)
        {
            for (int i = 0; i < E.Length; i++)
            {
                E[i] = Encrypt(E[i]);
            }
            return E;
        }
    }
}
