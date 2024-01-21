namespace Trade_Test.Utilities.Extensions
{
    public interface IEncryption
    {
        string Decrypt(string cipherText);
        string Encrypt(string plainText);
    }
}
