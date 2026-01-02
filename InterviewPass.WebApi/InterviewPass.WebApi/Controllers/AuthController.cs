using AutoMapper;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models.User;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthController(
        IUserAuthService authService,
        ITokenService tokenService,
        IMapper mapper)
    {
        _authService = authService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginViewModel model)
    {
        var user = _authService.Authenticate(model.Email, model.Password);

        if (user == null)
            return Unauthorized("Invalid email or password");

        return Ok(new
        {
            Token = _tokenService.GenerateToken(user),
            User = user.GetUserModel(_mapper),
               Message = "Login successful"
        });
    }
}
