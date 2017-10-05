using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class RCA
    {
        Utilities Ut = new Utilities();
        int PrivateKEy = 0;

        public string getPublicKey(int np, int nq) // n prime number
        {
            int p = Ut.GetPrimeNumber(np);
            int q = Ut.GetPrimeNumber(nq);
            int[] NyZ = Ut.Phi(p, q);
            int k = Ut.getFirstCoprime(NyZ[1], 50, 100);
            PrivateKEy = getPrivateKEy(k, NyZ[1], 100);
            return NyZ[0].ToString() + "," + k.ToString();
        }
        
        public int getPrivateKEy(int k, int z, int length)
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
