using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.DataAccess.Services
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        InterviewPassContext _dbContext;
        public JobSeekerRepository(DbContext dbContext) 
        {
            _dbContext = dbContext as InterviewPassContext;
        }
      
        public void AddUser(UserJobSeeker user)
        {
          _dbContext.UserJobSeekers.Add(user);
          _dbContext.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            _dbContext.UserJobSeekers.Remove(GetUser(id));
        }


        public UserJobSeeker GetUser(string login)
        {
            return _dbContext.UserJobSeekers.FirstOrDefault(x => x.Login == login);
        }

        public List<UserJobSeeker> GetUsers()
        {
            return _dbContext.UserJobSeekers.ToList();
        }
    }
}
