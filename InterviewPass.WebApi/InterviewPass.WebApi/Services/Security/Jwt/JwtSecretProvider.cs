using System.Security.Cryptography;
using System.Text;

public class JwtSecretProvider : IJwtSecretProvider
{
    private readonly IConfiguration _configuration;

    public JwtSecretProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetSecret()
    {
        string secret = _configuration["JwtSettings:SecretKey"]
            ?? throw new InvalidOperationException("JWT Secret not found in configuration.");
        return DpapiCrypto.Decrypt(secret) ;
    }
}