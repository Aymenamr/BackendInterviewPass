using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InterviewPass.DataAccess.Repositories
{
	public class JobRepository : IJobRepository
	{
		InterviewPassContext _dbContext;
		public JobRepository(DbContext dbContext)
		{
			_dbContext = dbContext as InterviewPassContext;
		}
		public List<Job> GetAllWithDetails()
		{
			return _dbContext.Jobs
					  .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
					  .Include(j => j.JobBenefits).ThenInclude(jb => jb.Benefit)
					  .Include(j => j.JobFiles)
					  .ToList();
		}

		public Job GetJobWithDetails(Expression<Func<Job, bool>> predicate)
		{
			return _dbContext.Jobs
		  .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
		  .Include(j => j.JobBenefits).ThenInclude(jb => jb.Benefit)
		  .Include(j => j.JobFiles)
		  .FirstOrDefault(predicate);
		}

		public void AddJob(Job job)
		{

			job.Id = Guid.NewGuid().ToString();
			_dbContext.Jobs.Add(job);
			_dbContext.SaveChanges();
		}

		public void UpdateJob(Job job)
		{
			_dbContext.Jobs.Update(job);
			_dbContext.SaveChanges();
		}

		public void DeleteJob(Job job)
		{

			if (job != null)
			{
				_dbContext.Jobs.Remove(job);
				_dbContext.SaveChanges();
			}
		}



	}
}
