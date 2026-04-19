using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;

namespace InterviewPass.WebApi.Validators.Result
{
    public interface IResultValidator
    {
        ApiResponse ValidateForCreate(ResultModel result);
        ApiResponse ValidateForUpdate(string id, ResultModel result);
    }
}
