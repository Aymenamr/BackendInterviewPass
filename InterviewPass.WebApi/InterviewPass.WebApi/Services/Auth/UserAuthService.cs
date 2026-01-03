using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;

public class UserAuthService : IUserAuthService
{
    private readonly Func<string, IUserRepository> _repoResolver;
    private readonly IPasswordService _passwordService;

    public UserAuthService(
        Func<string, IUserRepository> repoResolver,
        IPasswordService passwordService)
    {
        _repoResolver = repoResolver;
        _passwordService = passwordService;
    }

    public User? Authenticate(string email, string password)
    {
        var user = _repoResolver("JobSeeker").GetUserByEmail(email)
                   ?? _repoResolver("Hr").GetUserByEmail(email);

        if (user == null)
            return null;

        return _passwordService.Verify(user, password) ? user : null;
    }
}
