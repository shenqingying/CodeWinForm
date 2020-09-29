using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sonluk.Utility.Security
{
    public sealed class DESE
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="dataToEncrypt">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Encrypt(string dataToEncrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //把字符串放到byte数组中  
            byte[] inputByteArray = Encoding.Default.GetBytes(dataToEncrypt);

            //建立加密对象的密钥和偏移量  
            //使得输入密码必须输入英文文本 

            des.Key = ASCIIEncoding.ASCII.GetBytes(PrepareKey(key));
            des.IV = ASCIIEncoding.ASCII.GetBytes(PrepareKey(key));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:x2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="dataToDecrypt">待解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns> 
        public static string Decrypt(string dataToDecrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[dataToDecrypt.Length / 2];
            for (int x = 0; x < dataToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(dataToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改  
            des.Key = ASCIIEncoding.ASCII.GetBytes(PrepareKey(key));
            des.IV = ASCIIEncoding.ASCII.GetBytes(PrepareKey(key));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="strPlain">明文</param>
        /// <param name="strDESKey">密钥</param>
        /// <param name="strDESIV">向量</param>
        /// <returns>密文</returns>
        public static string DESEncrypt(string strPlain, string strDESKey, string strDESIV)
        {
            //把密钥转换成字节数组
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(strDESKey);
            //把向量转换成字节数组
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(strDESIV);
            //声明1个新的DES对象
            DESCryptoServiceProvider desEncrypt = new DESCryptoServiceProvider();
            //开辟一块内存流
            MemoryStream msEncrypt = new MemoryStream();
            //把内存流对象包装成加密流对象
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, desEncrypt.CreateEncryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Write);
            //把加密流对象包装成写入流对象
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            //写入流对象写入明文
            swEncrypt.WriteLine(strPlain);
            //写入流关闭
            swEncrypt.Close();
            //加密流关闭
            csEncrypt.Close();
            //把内存流转换成字节数组，内存流现在已经是密文了
            byte[] bytesCipher = msEncrypt.ToArray();
            //内存流关闭
            msEncrypt.Close();
            //把密文字节数组转换为字符串，并返回
            return UnicodeEncoding.Unicode.GetString(bytesCipher);
        }

        /// <summary>
        /// DES解密方法
        /// </summary>
        /// <param name="strCipher">密文</param>
        /// <param name="strDESKey">密钥</param>
        /// <param name="strDESIV">向量</param>
        /// <returns>明文</returns>
        public static string DESDecrypt(string strCipher, string strDESKey, string strDESIV)
        {
            //把密钥转换成字节数组
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(strDESKey);
            //把向量转换成字节数组
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(strDESIV);
            //把密文转换成字节数组
            byte[] bytesCipher = UnicodeEncoding.Unicode.GetBytes(strCipher);
            //声明1个新的DES对象
            DESCryptoServiceProvider desDecrypt = new DESCryptoServiceProvider();
            //开辟一块内存流，并存放密文字节数组
            MemoryStream msDecrypt = new MemoryStream(bytesCipher);
            //把内存流对象包装成解密流对象
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, desDecrypt.CreateDecryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Read);
            //把解密流对象包装成读出流对象
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            //明文=读出流的读出内容
            string strPlainText = srDecrypt.ReadLine();
            //读出流关闭
            srDecrypt.Close();
            //解密流关闭
            csDecrypt.Close();
            //内存流关闭
            msDecrypt.Close();
            //返回明文
            return strPlainText;
        }

        private static string PrepareKey(string key)
        {
            int length = key.Length;
            if (length < 8)
            {
                for (int i = length; i < 8; i++)
                {
                    key = key + i;
                }
            }
            else if (length > 8)
            {
                key = key.Substring(0, 8);
            }

            return key;
        }
    }
}
