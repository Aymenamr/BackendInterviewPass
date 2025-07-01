using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;

namespace InterviewPass.DataAccess.UnitOfWork
{
	public interface IUnitOfWork
	{
		IGenericRepository<Exam> ExamRepo { get; }
		IGenericRepository<Possibility> PossibilityRepo { get; }
		IGenericRepository<Question> QuestionRepo { get; }
		IGenericRepository<Job> JobRepo { get; }
        IGenericRepository<Answer> AnswerRepo { get; }

        void Save();
	}
}
