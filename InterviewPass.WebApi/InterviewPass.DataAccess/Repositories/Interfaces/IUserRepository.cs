using InterviewPass.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string id);
        List<User> GetUsers();
        void DeleteUser(string id);
        void AddUser(User user);
    }
}
