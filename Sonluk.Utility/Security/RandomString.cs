using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace Sonluk.Utility.Security
{
    public class RandomString
    {
        public string Create(int length)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + 1;
            Console.WriteLine(DateTime.Now.Ticks + ";" + DateTime.Now.Millisecond + ";" + Membership.GeneratePassword(8, 1));

            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> 1)));
            for (int i = 0; i < length; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }

            return str;
        }

        public string Create(int length,int numberOfNonAlphanumericCharacters)
        {
            return Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
        }

        private int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            Console.WriteLine(BitConverter.ToInt32(bytes, 0));
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
