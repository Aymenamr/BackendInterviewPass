using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IJobSeekerRepository
    {
        UserJobSeeker GetUser(string id);
        List<UserJobSeeker> GetUsers();
        void DeleteUser(string id);
        void AddUser(UserJobSeeker user);
    }
}
