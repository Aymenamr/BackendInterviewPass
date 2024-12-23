using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InterviewPass.DataAccess.Services
{
    public class HrRepository : IUserRepository
    {
        InterviewPassContext _dbContext;
        public HrRepository(DbContext dbContext) 
        {
            _dbContext = dbContext as InterviewPassContext;
        }
      
        public void AddUser(User user)
        {
            var hr = user as UserHr;
           user.Id = Guid.NewGuid().ToString();
          _dbContext.UserHrs.Add(hr);
          _dbContext.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            _dbContext.UserHrs.Remove(GetUser(id) as UserHr);
            _dbContext.SaveChanges();
        }


        public User GetUser(string login)
        {
            return _dbContext.UserHrs.FirstOrDefault(x => x.Login == login);
        }

        public List<User> GetUsers()
        {
            List <User> hrs = [.. _dbContext.UserHrs.ToList()];
            return hrs;
        }
    }
}
