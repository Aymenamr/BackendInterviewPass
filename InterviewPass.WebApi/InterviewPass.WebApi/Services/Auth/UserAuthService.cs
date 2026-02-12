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

    public User? Authenticate(string login, string password)
    {
        var user = _repoResolver(UserType.JobSeeker).GetUserByLogin(login)
                   ?? _repoResolver(UserType.Hr).GetUserByLogin(login);

        if (user == null)
            return null;

        return _passwordService.Verify(user, password) ? user : null;
    }
}
