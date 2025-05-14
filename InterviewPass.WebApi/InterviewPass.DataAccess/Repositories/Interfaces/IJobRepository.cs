using InterviewPass.DataAccess.Entities;
using System.Linq.Expressions;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
	public interface IJobRepository
	{
		List<Job> GetAllWithDetails();
		Job GetJobWithDetails(Expression<Func<Job, bool>> predicate);
		void AddJob(Job job);
		void UpdateJob(Job job);
		void DeleteJob(Job job);


	}
}
