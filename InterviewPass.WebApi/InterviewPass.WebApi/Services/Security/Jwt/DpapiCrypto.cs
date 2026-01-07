using System.Security.Cryptography;
using System.Text;

public static class DpapiCrypto
{
     private static readonly byte[] Entropy =
        Encoding.UTF8.GetBytes("MySuperSecretEntropyKey@2025!");

    public static string Encrypt(string plainText)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);

        var encryptedBytes = ProtectedData.Protect(
            plainBytes,
            Entropy,
            DataProtectionScope.LocalMachine
        );

        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string cipherText)
    {
        var encryptedBytes = Convert.FromBase64String(cipherText);

        var decryptedBytes = ProtectedData.Unprotect(
            encryptedBytes,
            Entropy,
            DataProtectionScope.LocalMachine
        );

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
