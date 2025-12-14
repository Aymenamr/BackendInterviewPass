using FluentValidation;
using InterviewPass.WebApi.Models.User;
using InterviewPass.DataAccess;
using InterviewPass.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Validators
{
    public class AddUsersRequestValidator : AbstractValidator<UserJobSeekerModel>
    {
        private readonly InterviewPassContext _dbContext;

        public AddUsersRequestValidator(DbContext dbContext)
        {
            _dbContext = dbContext as InterviewPassContext;

            RuleFor(x => x.Skills)
               .Must(ValidSkills)
               .When(x => x.Skills != null && x.Skills.Any())
               .WithMessage("Invalid Skill IDs provided. Some skills do not exist.");
        }

        private bool ValidSkills(ICollection<SkillModel> skills)
        {
            var skillIds = skills.Select(s => s.Id).ToList();
            var validSkillCount = _dbContext.Skills
               .Count(s => skillIds.Contains(s.Id));
            return validSkillCount == skillIds.Count;
        }
    }
}
