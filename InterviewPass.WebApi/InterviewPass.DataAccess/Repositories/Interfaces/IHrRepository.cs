using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IHrRepository
    {
        UserHr GetUser(string id);
        List<UserHr> GetUsers();
        void DeleteUser(string id);
        void AddUser(UserHr user);
    }
}
