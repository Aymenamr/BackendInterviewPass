
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
            exam.Id = Guid.NewGuid().ToString();
            _dbContext.Add(exam);
            _dbContext.SaveChanges();
        }

        public void UpdateExam(Exam exam)
        {
             _dbContext.Update(exam);
             _dbContext.SaveChanges();
        }

        public void DeleteExam(string id)
        {
            _dbContext.Remove(RetrieveExam(id));
            _dbContext.SaveChanges();
        }

        public List<Exam> RetrieveAll()
        {
            return _dbContext.Exams.ToList();
        }

        public Exam RetrieveExam(string id)
        {
            return _dbContext.Exams.FirstOrDefault(i => i.Id == id);
        }

        public Exam RetrieveExamByName(string name)
        {
            return _dbContext.Exams.ToList().FirstOrDefault(i => string.Equals(name,i.Name,StringComparison.OrdinalIgnoreCase));
        }
    }
}
