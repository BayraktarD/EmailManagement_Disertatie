using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace EmailMngmntApi.RSA
{
    public class RSAHelper : IRSAHelper
    {
        private readonly RSACryptoServiceProvider _frontendPrivateKey;
        private readonly RSACryptoServiceProvider _frontendPublicKey;

        private readonly RSACryptoServiceProvider _dbPrivateKey;
        private readonly RSACryptoServiceProvider _dbPublicKey;

        public RSAHelper()
        {
            string frontendPublicKey_pem = @".\keys\front.pub.pem";
            string frontendPrivateKey_pem = @".\keys\front.key.pem";

            string dbPublicKey_pem = @".\keys\db.pub.pem";
            string dbPrivateKey_pem = @".\keys\db.key.pem";

            _frontendPrivateKey = GetPrivateKeyFromPemFile(frontendPrivateKey_pem);
            _frontendPublicKey = GetPublicKeyFromPemFile(frontendPublicKey_pem);


            _dbPrivateKey = GetPrivateKeyFromPemFile(dbPrivateKey_pem);
            _dbPublicKey = GetPublicKeyFromPemFile(dbPublicKey_pem);
        }

        public string Encrypt(string text)
        {
            try
            {
                var encryptedBytes = _frontendPublicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Decrypt(string encrypted)
        {
            try
            {
                var decryptedBytes = _frontendPrivateKey.Decrypt(Convert.FromBase64String(encrypted), false);
                return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public string DatabaseEncrypt(string text)
        //{
        //    try
        //    {
        //        var encryptedBytes = _dbPublicKey.Encrypt(Encoding.UTF8.GetBytes(text), false);
        //        return Convert.ToBase64String(encryptedBytes);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public string DatabaseDecrypt(string encrypted)
        //{
        //    try
        //    {
        //        var decryptedBytes = _dbPrivateKey.Decrypt(Convert.FromBase64String(encrypted), false);
        //        return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}


        private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
        {
            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        private RSACryptoServiceProvider GetPublicKeyFromPemFile(String filePath)
        {
            using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }
    }
}
