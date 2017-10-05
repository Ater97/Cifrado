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

        public string PublicKey(int np, int nq) // n prime number
        {
            int p = Ut.GetPrimeNumber(np);
            int q = Ut.GetPrimeNumber(nq);
            int[] NyZ = Ut.Phi(p, q);
            int k = Ut.getFirstCoprime(NyZ[1], 20, 100);
            return NyZ[0].ToString() + "," + k.ToString();
        }
        //k* j = 1(mod z) Private key
    }
}
