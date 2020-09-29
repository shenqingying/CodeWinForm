using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Sonluk.Utility.Security
{
	public class RSAE
	{

        static public string Encrypt(string dataStr, string KeyInfoHexStr)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(HexStrToXmlStr(KeyInfoHexStr));
                return Encrypt(dataStr, RSA.ExportParameters(false));
            }
        }

		static public string Encrypt(string dataStr, RSAParameters KeyInfo)
		{
            byte[] data = Encoding.Default.GetBytes(dataStr);

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
			{
				RSA.ImportParameters(KeyInfo);

                int keySize = RSA.KeySize / 8;

                int bufferSize = keySize - 11;

                byte[] buffer = new byte[bufferSize];

                MemoryStream msReader= new MemoryStream(data);

                MemoryStream msWriter = new MemoryStream();

                int dataBlockLen = msReader.Read(buffer, 0, bufferSize);

                while (dataBlockLen > 0)
                {

                    byte[] dataBlock = new byte[dataBlockLen];

                    Array.Copy(buffer, 0, dataBlock, 0, dataBlockLen);

                    byte[] encryptedDataBlock = RSA.Encrypt(dataBlock, false);

                    msWriter.Write(encryptedDataBlock, 0, encryptedDataBlock.Length);

                    dataBlockLen = msReader.Read(buffer, 0, bufferSize);

                }

                msReader.Close();

                byte[] encryptedData = msWriter.ToArray();

                msWriter.Close();

                return Convert.ToBase64String(encryptedData);
			}
   
		}

        static public string Decrypt(string encryptedDataStr, string KeyInfoHexStr)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(HexStrToXmlStr(KeyInfoHexStr));
                return Decrypt(encryptedDataStr, RSA.ExportParameters(true));
            }
        }

        static public string Decrypt(string encryptedDataStr, RSAParameters KeyInfo)
		{
            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(KeyInfo);

                int keySize = RSA.KeySize / 8;

                byte[] buffer = new byte[keySize];

                MemoryStream msReader = new MemoryStream(encryptedData);

                MemoryStream msWriter = new MemoryStream();

                int dataBlockLen = msReader.Read(buffer, 0, keySize);

                while (dataBlockLen > 0)
                {

                    byte[] encryptedDataBlock = new byte[dataBlockLen];

                    Array.Copy(buffer, 0, encryptedDataBlock, 0, dataBlockLen);

                    byte[] dataBlock = RSA.Decrypt(encryptedDataBlock, false);

                    msWriter.Write(dataBlock, 0, dataBlock.Length);

                    dataBlockLen = msReader.Read(buffer, 0, keySize);

                }

                msReader.Close();
                byte[] data = msWriter.ToArray();
                msWriter.Close();

                return Encoding.Default.GetString(data);

            }

		}

        static public string XmlStrToHexStr(string xmlStr)
        {
            byte[] xml = Encoding.Default.GetBytes(xmlStr);

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < xml.Length; i++)
            {
                sBuilder.Append(xml[i].ToString("x2"));
            }

            return sBuilder.ToString();
        
        }

        static public string HexStrToXmlStr(string hexStr)
        {

            byte[] hex = new byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length / 2; i++)
            {
                hex[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
            }
            return Encoding.Default.GetString(hex);

        }

        static public string BytesToHexStr(byte[] bytes)
        {

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sBuilder.Append(bytes[i].ToString("x2"));
            }

            return sBuilder.ToString();

        }

        static public byte[] HexStrToBytes(string hexStr)
        {

            byte[] bytes = new byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length / 2; i++)
            {
                bytes[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);
            }
            return bytes;

        }
	}
}