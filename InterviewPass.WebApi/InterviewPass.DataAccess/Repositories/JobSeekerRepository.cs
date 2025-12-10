using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using InterviewPass.Infrastructure.Exceptions;
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

            // Validate skills BEFORE saving
            if (seeker.SkillBySeekers != null && seeker.SkillBySeekers.Any())
            {
                var skillIds = seeker.SkillBySeekers.Select(s => s.Id).ToList();

                var validSkillIds = _dbContext.Skills
                    .Where(s => skillIds.Contains(s.Id))
                    .Select(s => s.Id)
                    .ToList();
                var missingSkills = skillIds.Except(validSkillIds).ToList();
                if (missingSkills.Any())
                {
					throw new NotFoundException("Invalid Skill IDs provided. Some skills do not exist") ;
                }
            }
            seeker.Id = Guid.NewGuid().ToString();
            _dbContext.UserJobSeekers.Add(seeker);
            _dbContext.SaveChanges();

           
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

		public User GetUserByEmail(string email)
		{
			return _dbContext.UserHrs.FirstOrDefault(user => user.Email == email);
		}
		public List<User> GetUsers()
		{
			List<User> seekers = [.. _dbContext.UserJobSeekers.Include(user => user.SkillBySeekers).ThenInclude(sbs => sbs.Skill).ToList()];
			return seekers;
		}
	}
}
