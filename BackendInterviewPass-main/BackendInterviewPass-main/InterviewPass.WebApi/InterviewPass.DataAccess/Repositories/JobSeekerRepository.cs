using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.DataAccess.Services
{
    public class JobSeekerRepository : IUserRepository
    {
        InterviewPassContext _dbContext;
        public JobSeekerRepository(DbContext dbContext) 
        {
            _dbContext = dbContext as InterviewPassContext;
        }
      
        public void AddUser(User user)
        {
            var seeker = user as UserJobSeeker;
            seeker.Id = Guid.NewGuid().ToString();
            _dbContext.UserJobSeekers.Add(seeker);
            _dbContext.SaveChanges();
            user = GetUser(user.Login); // update the user entity object with the skills info
        }

        public void DeleteUser(string id)
        {
            _dbContext.UserJobSeekers.Remove(GetUser(id) as UserJobSeeker);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method to get a job seeker from the database
        /// according to the login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The user entity with all his skills included</returns>
        public User GetUser(string login)
        {
            return _dbContext.UserJobSeekers.Include(user => user.SkillBySeekers).ThenInclude(sbs => sbs.Skill).FirstOrDefault(user => user.Login == login);
        }

        public List<User> GetUsers()
        {
            List<User> seekers = [.. _dbContext.UserJobSeekers.Include(user => user.SkillBySeekers).ThenInclude(sbs => sbs.Skill).ToList()];
            return seekers;
        }
    }
}
