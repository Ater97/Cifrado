using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    class SDesAlgorithm
    {
        private int[][] Sl = new[]
        {
            new int[] {1, 0, 3, 2},
            new int[] {3, 2, 1, 0},
            new int[] {0, 2, 1, 3},
            new int[] {3, 1, 3, 1},
        };
        private int[][] Sr = new[]
        {
            new int[] {1, 1, 2, 3},
            new int[] {2, 0, 1, 3},
            new int[] {3, 0, 1, 0},
            new int[] {2, 1, 0, 3},
        };
        public IList<byte> k1 = new List<byte>(8);
        public IList<byte> k2 = new List<byte>(8);
        public IList<byte> DecToBytes(int value)
        {
            if (value < 0 || value > 1023)
            {
                return new List<byte>();
            }
            var result = new List<byte>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var stringButes = Convert.ToString(value, 2);
            for (int i = stringButes.Length - 1, j = 9; i >= 0; i--, j--)
            {
                result[j] = byte.Parse(stringButes[i].ToString());
            }
            return result;
        }

        #region key
        public IList<byte> getK1()
        {
            return k1;
        }
        public IList<byte> getK2()
        {
            return k2;
        }

        public void GenerateKeys(int key)
        {
            var bytes = DecToBytes(key);
            if (bytes.Count != 0)
            {
                bytes = ToP10(bytes);
                bytes = CyclicShift_One(bytes);
                k1 = ToP8(bytes);

                bytes = CyclicShift_Two(bytes);
                k2 = ToP8(bytes);
            }
        }

        public IList<byte> ToP10(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = key[2];
            result[1] = key[4];
            result[2] = key[1];
            result[3] = key[6];
            result[4] = key[3];
            result[5] = key[9];
            result[6] = key[0];
            result[7] = key[8];
            result[8] = key[7];
            result[9] = key[5];
            return result;
        }

        public IList<byte> ToP8(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 /*, 0, 0*/};
            result[0] = key[5];
            result[1] = key[2];
            result[2] = key[6];
            result[3] = key[3];
            result[4] = key[7];
            result[5] = key[4];
            result[6] = key[9];
            result[7] = key[8];
            return result;
        }

        public IList<byte> CyclicShift_One(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = key[1];
            result[1] = key[2];
            result[2] = key[3];
            result[3] = key[4];
            result[4] = key[0];
            result[5] = key[6];
            result[6] = key[7];
            result[7] = key[8];
            result[8] = key[9];
            result[9] = key[5];
            return result;
        }

        public IList<byte> CyclicShift_Two(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = key[2];
            result[1] = key[3];
            result[2] = key[4];
            result[3] = key[0];
            result[4] = key[1];
            result[5] = key[7];
            result[6] = key[8];
            result[7] = key[9];
            result[8] = key[5];
            result[9] = key[6];
            return result;
        }

        #endregion

        #region alg


        public IList<byte> StringToBytes(string key)
        {
            var a = Encoding.GetEncoding("cp866");
            var value = a.GetBytes(key)[0];
            if (value < 0 || value > 255)
            {
                throw new ArgumentException();
            }
            var result = new List<byte>() { 0, 0, 0, 0, 0, 0, 0, 0, };
            var stringButes = Convert.ToString(value, 2);
            for (int i = stringButes.Length - 1, j = 7; i >= 0; i--, j--)
            {
                result[j] = byte.Parse(stringButes[i].ToString());
            }
            return result;

        }

        public IList<byte> ToBinary(byte value)
        {
            if (value < 0 || value > 255)
            {
                throw new ArgumentException();
            }
            var result = new List<byte>() { 0, 0, 0, 0, 0, 0, 0, 0, };
            var stringButes = Convert.ToString(value, 2);
            for (int i = stringButes.Length - 1, j = 7; i >= 0; i--, j--)
            {
                result[j] = byte.Parse(stringButes[i].ToString());
            }
            return result;
        }

        public IList<byte> ToIP(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = key[1];
            result[1] = key[5];
            result[2] = key[2];
            result[3] = key[0];
            result[4] = key[3];
            result[5] = key[7];
            result[6] = key[4];
            result[7] = key[6];
            return result;
        }

        public IList<byte> ToIP_1(IList<byte> key)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = key[3];
            result[1] = key[0];
            result[2] = key[2];
            result[3] = key[4];
            result[4] = key[6];
            result[5] = key[1];
            result[6] = key[7];
            result[7] = key[5];
            return result;
        }

        public IList<byte> EP(IList<byte> r)
        {
            var result = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };
            result[0] = r[3];
            result[1] = r[0];
            result[2] = r[1];
            result[3] = r[2];
            result[4] = r[1];
            result[5] = r[2];
            result[6] = r[3];
            result[7] = r[0];
            return result;
        }

        public IList<byte> XOR(IList<byte> a, IList<byte> b)
        {
            if (a.Count != b.Count) throw new ArgumentException();
            var result = new List<byte>();
            for (var i = 0; i < a.Count; i++)
            {
                var x = (byte)(a[i] ^ b[i]);
                result.Add(x);
            }
            return result;
        }

        public IList<byte> SMatrix(IList<byte> bytes)
        {
            var line1 = Convert.ToInt32((bytes[0] * 10 + bytes[3]).ToString(), 2);
            var column1 = Convert.ToInt32((bytes[1] * 10 + bytes[2]).ToString(), 2);

            var line2 = Convert.ToInt32((bytes[4] * 10 + bytes[7]).ToString(), 2);
            var column2 = Convert.ToInt32((bytes[5] * 10 + bytes[6]).ToString(), 2);

            var sl = Sl[line1][column1];
            var sr = Sl[line2][column2];

            var result = new List<byte>();
            result.AddRange(ToBytes2(sl));
            result.AddRange(ToBytes2(sr));
            return result;
        }

        public IList<byte> P4(IList<byte> r)
        {
            var result = new List<byte> { 0, 0, 0, 0 };
            result[0] = r[1];
            result[1] = r[3];
            result[2] = r[2];
            result[3] = r[0];
            return result;
        }

        private IList<byte> ToBytes2(int val)
        {
            var result = new List<byte>() { 0, 0 };
            var stringButes = Convert.ToString(val, 2);
            for (int i = stringButes.Length - 1, j = 1; i >= 0; i--, j--)
            {
                result[j] = byte.Parse(stringButes[i].ToString());
            }
            return result;
        }

        public IList<byte> SW(IList<byte> xor, IList<byte> r)
        {
            var result = new List<byte>();
            result.AddRange(r);
            result.AddRange(xor);
            return result;
        }

        public IList<byte> Fk(IList<byte> bytes, IList<byte> sk)
        {
            var l = bytes.Take(4).ToList();
            var r = bytes.Skip(4).Take(4).ToList();
            var ep = EP(r);

            var xor = XOR(ep, sk);

            var sm = SMatrix(xor);
            var p4 = P4(sm);

            var xor2 = XOR(l, p4);

            return xor2;
        }

        public byte Encript(byte bytesToEncript)
        {
            IList<byte> bytes = ToBinary(bytesToEncript);

            var ip = ToIP(bytes);

            var fk1 = Fk(ip, k1);

            var sw = SW(fk1, ip.Skip(4).Take(4).ToList());

            var fk2 = Fk(sw, k2);

            var sw2 = SW(fk1, fk2);

            var ip_1 = ToIP_1(sw2);

            var stringBin = string.Join(string.Empty, ip_1);
            var dec = Convert.ToInt32(stringBin, 2);
            return (byte)dec;
        }

        public byte Decript(byte bytesToDecrypt)
        {
            IList<byte> bytes = ToBinary(bytesToDecrypt);

            var ip = ToIP(bytes);

            var fk1 = Fk(ip, k2);

            var sw = SW(fk1, ip.Skip(4).Take(4).ToList());

            var fk2 = Fk(sw, k1);

            var sw2 = SW(fk1, fk2);

            var ip_1 = ToIP_1(sw2);
            
            var stringBin = string.Join(string.Empty, ip_1);
            var dec = Convert.ToInt32(stringBin, 2);
            return (byte)dec;
        }

        public string Encript(string symbol)
        {
            IList<byte> bytes = StringToBytes(symbol);

            var ip = ToIP(bytes);

            var fk1 = Fk(ip, k1);

            var sw = SW(fk1, ip.Skip(4).Take(4).ToList());

            var fk2 = Fk(sw, k2);

            var sw2 = SW(fk1, fk2);

            var ip_1 = ToIP_1(sw2);

            var stringBin = string.Join(string.Empty, ip_1);
            var dec = Convert.ToInt32(stringBin, 2);
            var a = Encoding.GetEncoding("cp866");
            var text = a.GetString(new[] { (byte)dec });
            return text;
        }
        
        public string Decript(string symbol)
        {
            IList<byte> bytes = StringToBytes(symbol);

            var ip = ToIP(bytes);

            var fk1 = Fk(ip, k2);

            var sw = SW(fk1, ip.Skip(4).Take(4).ToList());

            var fk2 = Fk(sw, k1);

            var sw2 = SW(fk1, fk2);

            var ip_1 = ToIP_1(sw2);

            var stringBin = string.Join(string.Empty, ip_1);
            var dec = Convert.ToInt32(stringBin, 2);
            var a = Encoding.GetEncoding("cp866");
            var text = a.GetString(new[] { (byte)dec });
            return text;
        }

        #endregion
    }
}
