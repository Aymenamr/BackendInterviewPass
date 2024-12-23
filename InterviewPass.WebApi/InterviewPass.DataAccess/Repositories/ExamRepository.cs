
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InterviewPass.DataAccess.Repositories
{
    public class ExamRepository : IExamRepository
    {
        InterviewPassContext _dbContext;
        public ExamRepository(DbContext dbContext)
        {
            _dbContext = dbContext as InterviewPassContext;
        }
        public void AddExam(Exam exam)
        {
        }

        public void UpdateExam(Exam exam)
        {
         
        }

        public void DeleteExam(int id)
        {
        }

        public IEnumerable<Exam> RetrieveAll()
        {
            return null;
        }

        public Exam RetrieveExam(int id)
        {
            return null;
        }
    }
}
