using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		public IGenericRepository<Exam> ExamRepo { get; }
		public IGenericRepository<Question> QuestionRepo { get; }
		public IGenericRepository<Possibility> PossibilityRepo { get; }
		public IGenericRepository<Job> JobRepo { get; }

		private readonly DbContext _dbContext;

		public UnitOfWork(DbContext dbContext)
		{
			_dbContext = dbContext;
			ExamRepo = new GenericRepository<Exam>(_dbContext);
			QuestionRepo = new GenericRepository<Question>(_dbContext);
			PossibilityRepo = new GenericRepository<Possibility>(_dbContext);
			JobRepo = new GenericRepository<Job>(_dbContext);
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}

	}
}
