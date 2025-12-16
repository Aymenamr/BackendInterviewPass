using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;

namespace InterviewPass.WebApi.Validators.Exam
{
    public interface IExamValidator
    {
        ApiResponse Validate(ExamModel exam);
    }

}
