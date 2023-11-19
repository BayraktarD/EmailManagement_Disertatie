using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EmailMngmntApi.AES
{
    public class AESHelper : IAESHelper
    {

        public AESHelper() { }

        private readonly string _ivKey = "@qwertyuiop12344";
        private readonly string _aesKey = "";

        public string Decrypt(string encryptedValue, string aesKey)
        {
            var encrypted = Convert.FromBase64String(encryptedValue);
            return DecryptStringFromBytes(encrypted, Encoding.UTF8.GetBytes(aesKey), Encoding.UTF8.GetBytes(_ivKey)).ToString();
        }

        public string GetKey()
        {
            return generateKey(16);
        }



        private string generateKey(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("Cipher Text");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("Iv");

            string plaintext = null;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.ECB;
                rijAlg.Padding = PaddingMode.ISO10126;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    using var msDecrypt = new MemoryStream(cipherText);
                    using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using var srDecrypt = new StreamReader(csDecrypt);
                    plaintext = srDecrypt.ReadToEnd();
                }
                catch (Exception)
                {
                    plaintext = "Invalid Key";
                }

            }

            return plaintext;
        }

        public string Encrypt(string plainText, string key)
        {
            //byte[] encrypted;
            string encrypted;

            using (var rijAlg = new RijndaelManaged())
            {

                rijAlg.Mode = CipherMode.ECB;
                rijAlg.Padding = PaddingMode.ISO10126;
                rijAlg.FeedbackSize = 128;


                rijAlg.Key = Encoding.UTF8.GetBytes(key);
                rijAlg.IV = Encoding.UTF8.GetBytes(_ivKey);


                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Create crypto stream using the CryptoStream class. This class is the key to encryption
                        // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                        // to encrypt
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            // Create StreamWriter and write data to a stream
                            using (StreamWriter sw = new StreamWriter(cs))
                                sw.Write(plainText);

                            encrypted = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    encrypted = ex.Message;
                }

            }

            return encrypted;
        }
    }
}
