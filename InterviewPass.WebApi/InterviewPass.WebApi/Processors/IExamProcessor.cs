using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
    public interface IExamProcessor
    {
        void ProcessExam(ExamModel exam);
    }
}
