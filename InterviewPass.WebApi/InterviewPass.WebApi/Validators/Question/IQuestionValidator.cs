using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.ResponseResult;

namespace InterviewPass.WebApi.Validators.Question
{
    public interface IQuestionValidator
    {
        ApiResponse ValidateForCreate(QuestionModel question);
        ApiResponse ValidateForUpdate(string id, QuestionModel question);
    }
}
