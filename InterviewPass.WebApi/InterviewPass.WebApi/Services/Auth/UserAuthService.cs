using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Enums;

public class UserAuthService : IUserAuthService
{
    private readonly Func<UserType, IUserRepository> _repoResolver;
    private readonly IPasswordService _passwordService;

    public UserAuthService(
        Func<UserType, IUserRepository> repoResolver,
        IPasswordService passwordService)
    {
        _repoResolver = repoResolver;
        _passwordService = passwordService;
    }

    public User? Authenticate(string email, string password)
    {
        var user = _repoResolver(UserType.JobSeeker).GetUserByEmail(email)
                   ?? _repoResolver(UserType.Hr).GetUserByEmail(email);

        if (user == null)
            return null;

        return _passwordService.Verify(user, password) ? user : null;
    }
}
