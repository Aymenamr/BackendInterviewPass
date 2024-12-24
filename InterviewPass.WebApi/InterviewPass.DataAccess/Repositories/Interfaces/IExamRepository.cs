
using InterviewPass.DataAccess.Entities;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IExamRepository
    {
        List<Exam> RetrieveAll();
        Exam RetrieveExam(string id);
        void DeleteExam(string id);
        void AddExam(Exam exam);

    }
}
