using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    static class UtilitiesForRSA
    {
        public static int GetPrimeNumber(int num)
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

        public static int[] Phi(int p, int q)
        {
            int[]NyZ = new int[2];
            NyZ[0] = p * q;
            NyZ[1] = (p - 1) * (q - 1);
            return NyZ;
        }

        private static bool CheckCoprime(int A,int B)
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

        public static int getFirstCoprime(int A, int inicial)
        {
            bool flag = true;
            int i = inicial;
            while (flag)
            {
                if (CheckCoprime(A, i))
                    return i;
                i++;
            }
            return 0;
        }


    }
}
    
