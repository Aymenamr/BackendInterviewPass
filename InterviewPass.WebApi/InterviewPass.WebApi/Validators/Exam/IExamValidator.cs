using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Validators.Exam
{
    public interface IExamValidator
    {
        void Validate(ExamModel exam);
    }

}
