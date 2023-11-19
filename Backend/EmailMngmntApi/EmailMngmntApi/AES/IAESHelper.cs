namespace EmailMngmntApi.AES
{
    public interface IAESHelper
    {
        string Decrypt(string encryptedValue, string aesKey);
        string Encrypt(string plainText, string key);
        string GetKey();
    }
}
