using InterviewPass.DataAccess.Entities;

public interface IPasswordService
{
    bool Verify(User user, string password);

    string Hash(User user, string password);

}
