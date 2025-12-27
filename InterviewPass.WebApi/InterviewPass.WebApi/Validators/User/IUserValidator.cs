using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;
using InterviewPass.WebApi.Models.User;

namespace InterviewPass.WebApi.Validators.user
{
    public interface IUserValidator
    {
        ApiResponse Validate(UserModel skill);

     }
}
