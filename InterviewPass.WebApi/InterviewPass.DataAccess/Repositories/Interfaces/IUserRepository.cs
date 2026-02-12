using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User GetUser(string id);
		User GetUserByLogin(string login);
		List<User> GetUsers();
		void DeleteUser(string id);
		void AddUser(User user);
	}
}
