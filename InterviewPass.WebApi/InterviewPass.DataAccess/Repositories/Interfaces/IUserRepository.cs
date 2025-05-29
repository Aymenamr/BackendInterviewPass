using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User GetUser(string id);
		User GetUserByEmail(string email);
		List<User> GetUsers();
		void DeleteUser(string id);
		void AddUser(User user);
	}
}
