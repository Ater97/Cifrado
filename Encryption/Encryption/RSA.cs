using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    static class RSA
    {
        static int PrivateKEy = 0;

        

        public static string getPublicKey(int np, int nq) // n prime number
        {
            int p = UtilitiesForRSA.GetPrimeNumber(np);
            int q = UtilitiesForRSA.GetPrimeNumber(nq);
            int[] NyZ = UtilitiesForRSA.Phi(p, q);
            int k = UtilitiesForRSA.getFirstCoprime(NyZ[1], 50, 100);
            PrivateKEy = getPrivateKEy(k, NyZ[1], 100);
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
    }
}
