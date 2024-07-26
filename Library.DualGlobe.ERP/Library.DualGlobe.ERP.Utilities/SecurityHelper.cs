using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Library.DualGlobe.ERP.Utilities
{
	public class SecurityHelper
	{
		public SecurityHelper()
		{
		}

		public static string Decrypt(string toDecrypt)
		{
			byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
			string key = ConfigurationManager.AppSettings["EncryptKey"];
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] keyArray = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
			mD5CryptoServiceProvider.Clear();
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider()
			{
				Key = keyArray,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};
			byte[] resultArray = tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(toEncryptArray, 0, (int)toEncryptArray.Length);
			tripleDESCryptoServiceProvider.Clear();
			return Encoding.UTF8.GetString(resultArray);
		}

		public static int DecryptLicense(string encyptedValue)
		{
			string EncryptionKey = "Ihc87aSS89aGaJS";
			byte[] cipherBytes = Convert.FromBase64String(encyptedValue);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, (int)cipherBytes.Length);
						cs.Close();
					}
					encyptedValue = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return Convert.ToInt32(encyptedValue);
		}

		public static string Encrypt(string toEncrypt)
		{
			byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
			string key = ConfigurationManager.AppSettings["EncryptKey"];
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] keyArray = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
			mD5CryptoServiceProvider.Clear();
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider()
			{
				Key = keyArray,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};
			byte[] resultArray = tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(toEncryptArray, 0, (int)toEncryptArray.Length);
			tripleDESCryptoServiceProvider.Clear();
			return Convert.ToBase64String(resultArray, 0, (int)resultArray.Length);
		}
	}
}