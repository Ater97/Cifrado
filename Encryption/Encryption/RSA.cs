using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Encryption
{
    static class RSA
    {
        private static int PrivateKey = 0; //n,d
        private static int[] PublicKey = new int[2];//n,e
        private static int UserName = 0;
        /*
         *  n = p * q 
         *   Φ(n) = (p - 1) * (q - 1) 
         *    1 < e < Φ(n) 
         *     d = inv(e, Φ(n)) ⇒ mcd[e, Φ(n)] = 1
         *     (n, d) ⇒ Clave privada 
         *      (n, e) ⇒ Clave pública

         */

        public static string getPublicKey(int np, int nq) // n prime number
        {
            int p = UtilitiesForRSA.GetPrimeNumber(np);
            int q = UtilitiesForRSA.GetPrimeNumber(nq);
            int N = PublicKey[0] = p * q; //n,e
            int phi = (p - 1) * (q - 1);
            //
            int e = PublicKey[1] = UtilitiesForRSA.getFirstCoprime(phi, 7 * p); //primo relativo entre 1 y phi(n)
            //e * d = 1 mod(phi(n))
            int d = PrivateKey = getPrivateKey(phi, e, 7); //n,d

            return PublicKey[0].ToString() + "," + PublicKey[1].ToString();
        }

        public static string getPrivateKey()
        {
            return PublicKey[0].ToString() + "," + PrivateKey.ToString();
            PrivateKey = 0;
        }
        public static void setPrivateKey(int key)
        {
            PrivateKey = key;
        }
        public static void setN(int N)
        {
            PublicKey[0] = N;
        }

        public static int getPrivateKey(int phi, int e, int o)
        {
            bool flag = true;
            int i = o;
            while (flag)
            {
                if ((((e * i) % phi) == 1))
                    return i;
                i++;
            }
            return 0;
        }

        #region Int

        public static int EncriptKEYint(int key)
        {
            string keys = "Public key: " + getPublicKey(key, key * key) + Environment.NewLine;
            keys += "Private key :" + getPrivateKey();
            Console.WriteLine(keys);
            return EncriptKey(key);
        }

        public static int DecriptKEYint(int key, int privateKEY, int N)
        {
            setN(N);
            setPrivateKey(privateKEY);
            return DecryptKey(key);
        }

        public static int EncriptKey(int key)
        {
            BigInteger P2 = BigInteger.Pow(key, PublicKey[1]);
            P2 %= PublicKey[0];
            return (int)(P2);
        }
        public static int DecryptKey(int Dat)
        {//dat^d mod(n)
            BigInteger P2 = BigInteger.Pow(Dat, PrivateKey);
            P2 %= PublicKey[0];
            return (int)P2;
        }

        #endregion
        #region byte
        private static byte Encrypt(byte dat)
        { //dat^e mod (n)
            int P = dat;
            BigInteger P2 = BigInteger.Pow(P, PublicKey[1]);
            try
            {
                do
                {
                    P2 %= PublicKey[0];
                }
                while (P2 > 255);
            }
            catch
            {
                return (byte)(P2 - 255);
            }
            return (byte)P2;
        }

        public static byte[] Encrypt(byte[] E)
        {
            for (int i = 0; i < E.Length; i++)
            {
                E[i] = Encrypt(E[i]);
            }
            return E;
        }

        private static byte Decrypt(byte Dat)
        {//dat^d mod(n)
            int P = (Dat);
            BigInteger P2 = BigInteger.Pow(P, PrivateKey);
            do
            {
                P2 %= PublicKey[0];
            }
            while (P2 > 255);
            return (byte)P2;
        }

        public static byte[] Decrypt(byte[] E)
        {
            for (int i = 0; i < E.Length; i++)
            {
                E[i] = Decrypt(E[i]);
            }
            return E;
        }
        #endregion
    }
}


