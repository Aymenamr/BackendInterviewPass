using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;

namespace InterviewPass.WebApi.Validators.Skill
{
    public interface ISkillValidator
    {
        ApiResponse Validate(SkillModel skill);
    }
}
