using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

public class JwtSecretProvider : IJwtSecretProvider
{
    private readonly IConfiguration _configuration;
    private readonly byte[] _entropy;

    public JwtSecretProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _entropy = Encoding.UTF8.GetBytes("MySuperSecretEntropyKey@2025!");
    }

    public string GetSecret()
    {
        bool tagProtected = bool.TryParse(
            _configuration["JwtSettings:TagProtected"], out var tag) && tag;

        string secret = _configuration["JwtSettings:SecretKey"]!;

        if (tagProtected)
        {
            byte[] encryptedBytes = Convert.FromBase64String(secret);
            byte[] decryptedBytes = ProtectedData.Unprotect(
                encryptedBytes,
                _entropy,
                DataProtectionScope.LocalMachine
            );

            return Encoding.UTF8.GetString(decryptedBytes);
        }

         byte[] plainBytes = Encoding.UTF8.GetBytes(secret);
        byte[] encryptedBytesNew = ProtectedData.Protect(
            plainBytes,
            _entropy,
            DataProtectionScope.LocalMachine
        );

        string encryptedBase64 = Convert.ToBase64String(encryptedBytesNew);

        UpdateAppSettings(encryptedBase64);

        return secret; // return original plain secret for current startup
    }

    private void UpdateAppSettings(string encryptedSecret)
    {
        var jsonFilePath = "appsettings.json";
        var jsonText = File.ReadAllText(jsonFilePath);

        dynamic jsonObj = JsonConvert.DeserializeObject(jsonText);

        jsonObj["JwtSettings"]["SecretKey"] = encryptedSecret;
        jsonObj["JwtSettings"]["TagProtected"] = "true";

        string updatedJson = JsonConvert.SerializeObject(
            jsonObj,
            Formatting.Indented
        );

        File.WriteAllText(jsonFilePath, updatedJson);
    }
}
