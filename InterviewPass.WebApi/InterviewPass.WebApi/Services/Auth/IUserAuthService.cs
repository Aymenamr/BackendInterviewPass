using InterviewPass.DataAccess.Entities;

public interface IUserAuthService
{
    User Authenticate(string email, string password);
}
