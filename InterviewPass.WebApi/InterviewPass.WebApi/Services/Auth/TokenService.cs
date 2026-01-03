using InterviewPass.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IJwtSecretProvider _jwtSecretProvider;

    public TokenService(IConfiguration configuration  , IJwtSecretProvider jwtSecretProvider)
    {
        _configuration = configuration;
        _jwtSecretProvider = jwtSecretProvider;
    }

    public string GenerateToken(User user)
    {

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSecretProvider.GetSecret())
        );

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("UserType", user.GetType().ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JwtSettings:ExpiryInHours"])),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials( key,
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
