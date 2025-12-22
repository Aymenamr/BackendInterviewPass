using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors.Skill
{
    public interface ISkillProcessor
    {
        SkillModel ProcessSkill(SkillModel skill);
    }
}
