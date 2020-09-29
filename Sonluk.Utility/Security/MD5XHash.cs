using System;
using System.Security.Cryptography;
using System.Text;

namespace Sonluk.Utility.Security
{
    public class MD5XHash
    {
        static public string GetMD5XHash(int sourceInt)
        {
            return GetMD5XHash(sourceInt.ToString());
        }

        static public string GetMD5XHash(string sourceString)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sourceString));
                               
                for (int i = 0; i < data.Length; i++)
                {
                    int start = 0;
                    if (Int32.TryParse((data[i].ToString("x2")), out start))
                    {
                        start = start % 16;

                        int end = data.Length - 1;
                        for (int k = data.Length; k > 0; k--)
                        {
                            if (Int32.TryParse((data[k - 1].ToString("x2")), out end))
                            {
                                end = end % 16;
                                break;
                            }
                        }

                        byte temp = data[start];
                        data[start] = data[end];
                        data[end] = temp;
                        break;
                    }                                      
                }

                data = md5Hash.ComputeHash(data);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        static public bool VerifyMD5XHash(string sourceString, string sourceStrinHash)
        {
            string hashOfSourceString = GetMD5XHash(sourceString);
            
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfSourceString, sourceStrinHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}


