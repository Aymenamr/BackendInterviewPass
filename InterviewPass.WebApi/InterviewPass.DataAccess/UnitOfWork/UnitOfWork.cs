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
		public IGenericRepository<Skill> SkillRepo { get; }
		public IGenericRepository<JobBenefit> JobBenefitRepo { get; }
		public IGenericRepository<JobFile> JobFileRepo { get; }
		public IGenericRepository<JobSkill> JobSkillRepo { get; }
		public IGenericRepository<Benefits> BenefitRepo { get; }
        //do result repo for what ?? 
		public IGenericRepository<Result> ResultRepo { get; }

        public IGenericRepository<Result> resultRepo { get; }

        private readonly DbContext _dbContext;

		public UnitOfWork(DbContext dbContext)
		{
			_dbContext = dbContext;
			ExamRepo = new GenericRepository<Exam>(_dbContext);
			QuestionRepo = new GenericRepository<Question>(_dbContext);
			PossibilityRepo = new GenericRepository<Possibility>(_dbContext);
			JobRepo = new GenericRepository<Job>(_dbContext);
			SkillRepo = new GenericRepository<Skill>(_dbContext);
			JobBenefitRepo = new GenericRepository<JobBenefit>(_dbContext);
			JobFileRepo = new GenericRepository<JobFile>(_dbContext);
			BenefitRepo = new GenericRepository<Benefits>(_dbContext);
			JobSkillRepo = new GenericRepository<JobSkill>(_dbContext);
            ResultRepo = new GenericRepository<Result>(_dbContext);
        }

		public void Save()
		{
			_dbContext.SaveChanges();
			
		}

	}
}
