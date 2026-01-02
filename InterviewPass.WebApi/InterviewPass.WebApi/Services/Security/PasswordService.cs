using InterviewPass.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool Verify(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        );

        return result != PasswordVerificationResult.Failed;
    }

    public string Hash(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }
}
