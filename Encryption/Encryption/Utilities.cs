using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class Utilities
    {
        public int GetPrimeNumber(int num)
        {
            int i, n = 4, cont = 2;
            if (num > 2) // 2 & 3, too low.
            {
                while (cont < num)
                {
                    i = 2;
                    while (i <= n)
                    {
                        if (i == n)
                        {
                            cont = cont + 1;
                        }
                        else
                        {
                            if (n % i == 0)
                            {
                                i = n;
                            }
                        }
                        i = i + 1;
                    }
                    n = n + 1;
                }
                return n - 1;
            }
            return 3;
        }

        public int[] Phi(int p, int q)
        {
            int[]NyZ = new int[2];
            NyZ[0] = p * q;
            NyZ[1] = (p - 1) * (q - 1);
            return NyZ;
        }

        private bool CheckCoprime(int A,int B)
        {
            while (A != 0 && B != 0)
            {
                if (A > B)
                    A %= B;
                else
                    B %= A;
            }

            return Math.Max(A, B) == 1 ? true:false;

        }

        public int getFirstCoprime(int A, int inicial, int final)
        {
            for (int i = inicial; i < final ; i++)
            {
                if (CheckCoprime(A, i))
                    return i;
            }
            return 0;
        }


    }
}
    
