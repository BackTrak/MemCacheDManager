using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MemCacheDManager
{
	public class Encryption
	{
		private static byte[] _key = System.Text.ASCIIEncoding.ASCII.GetBytes("somesimplekey!!!");
		private static byte[] _iv = System.Text.ASCIIEncoding.ASCII.GetBytes("anothersomesimpl");

		public static string Encrypt(string value)
		{
			RijndaelManaged rm = new RijndaelManaged();

			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rm.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
			StreamWriter streamWriter = new StreamWriter(cryptoStream);
			streamWriter.Write(value);

			streamWriter.Flush();
			cryptoStream.FlushFinalBlock();

			return Convert.ToBase64String(memoryStream.ToArray());
		}

		public static string Decrypt(string value)
		{
			if (value == String.Empty)
				return value;

			RijndaelManaged rm = new RijndaelManaged();

			MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value));
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rm.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
			StreamReader streamReader = new StreamReader(cryptoStream);

			return streamReader.ReadToEnd();
		}
	}
}
