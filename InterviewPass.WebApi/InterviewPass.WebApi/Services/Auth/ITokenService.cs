using InterviewPass.DataAccess.Entities;

public interface ITokenService
{
    string GenerateToken(User user);
}
