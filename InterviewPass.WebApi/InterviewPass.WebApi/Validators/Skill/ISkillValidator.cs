using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Validators.Skill
{
    public interface ISkillValidator
    {
        void Validate(SkillModel skill);
    }
}
