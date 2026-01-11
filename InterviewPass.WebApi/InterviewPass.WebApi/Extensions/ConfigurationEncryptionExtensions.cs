using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class ConfigurationEncryptionExtensions
{
    public static void EncryptJwtSecret(
        this IConfiguration configuration,
        string filePath = "appsettings.json")
    {
        bool isProtected =
            configuration.GetValue<bool>("JwtSettings:TagProtected");

        if (isProtected) return;

        string? plainSecret =
            configuration["JwtSettings:SecretKey"];

        if (string.IsNullOrWhiteSpace(plainSecret)) return;

        string encrypted = DpapiCrypto.Encrypt(plainSecret);

        var json = JObject.Parse(File.ReadAllText(filePath));

        json["JwtSettings"]!["SecretKey"] = encrypted;
        json["JwtSettings"]!["TagProtected"] = true;

        File.WriteAllText(
            filePath,
            json.ToString(Formatting.Indented)
        );
    }
}
