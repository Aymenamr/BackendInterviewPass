using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewPass.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		private readonly Func<string, IUserRepository> _userRepoResolver;
		private readonly IConfiguration _configuration;

		private readonly IMapper _mapper;
		public AuthController(IConfiguration configuration, Func<string, IUserRepository> userRepoResolver, IMapper mapper)
		{
			_configuration = configuration;
			_userRepoResolver = userRepoResolver;
			_mapper = mapper;

		}
		[HttpPost("login")]
		[ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
		public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
		{

			// Find user by email/login
			var user = _userRepoResolver("JobSeeker").GetUserByEmail(loginModel.Email) ??
					   _userRepoResolver("Hr").GetUserByEmail(loginModel.Email);

			if (user == null)
			{
				return Unauthorized("Invalid email or password");
			}
			// Decode Base64 to get the original hash from database
			string originalHashFromDB;

			byte[] hashBytes = Convert.FromBase64String(user.PasswordHash);
			originalHashFromDB = Encoding.UTF8.GetString(hashBytes);

			// Frontend sends hashed password, compare hashes directly
			// loginModel.Password is already hashed by frontend 
			if (originalHashFromDB != loginModel.Password)
			{
				return Unauthorized("Invalid password");
			}
			var token = GenerateJwtToken(user);

			return Ok(new
			{
				Token = token,
				User = user.GetUserModel(_mapper),
				Message = "Login successful"
			});


		}

		private string GenerateJwtToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

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
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
