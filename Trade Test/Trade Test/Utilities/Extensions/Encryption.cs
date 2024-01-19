using System.Security.Cryptography;
using System.Text;

namespace Trade_Test.Utilities.Extensions
{
    public class Encryption : IEncryption
    {
        private readonly string _aesEncryptionKey;

        public Encryption(string aesEncryptionKey)
        {
            _aesEncryptionKey = aesEncryptionKey;
        }


        public string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_aesEncryptionKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_aesEncryptionKey);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static bool IsBase64(string base64String)
        {
            var stringsToCompare = new List<string> { " ", "\t", "\r", "\n" };
            bool isNotBase64 = string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0 || stringsToCompare.Contains(base64String);
            var result = false;

            if (isNotBase64)
            {
                return result;
            }

            try
            {
                var bytes = Convert.FromBase64String(base64String);
                result = bytes.Length > 0;
            }
            catch
            {

            }

            return result;
        }
    }
}
