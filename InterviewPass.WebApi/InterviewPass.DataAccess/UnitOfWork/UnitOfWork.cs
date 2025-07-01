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
        public IGenericRepository<Answer> AnswerRepo { get; }

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
            AnswerRepo = new GenericRepository<Answer>(_dbContext);

        }

        public void Save()
		{
            try
            {
                 _dbContext.SaveChanges();
               
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("DB Error: " + ex.InnerException?.Message);
                throw; // لإعادة رمي الخطأ بعد الطباعة
            }


        }

	}
}
