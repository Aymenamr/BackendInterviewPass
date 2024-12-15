
using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IExamRepository
    {
        IEnumerable<Exam> RetrieveAll();
        Exam RetrieveExam(int id);
        void DeleteExam(int id);
        void AddExam(Exam exam);

    }
}
