using FluentValidation;
using FluentValidation.Results;
using InterviewPass.WebApi.Models.User;
using InterviewPass.DataAccess;
using InterviewPass.WebApi.Enums;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Constants;
using InterviewPass.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace InterviewPass.WebApi.Validators
{
    public class AddUsersRequestValidator : AbstractValidator<UserModel>
    {
        private readonly InterviewPassContext _dbContext;
        private readonly Func<string, IUserRepository> _userRepoResolver;

        public AddUsersRequestValidator(DbContext dbContext, Func<string, IUserRepository> userRepoResolver)
        {
            _dbContext = dbContext as InterviewPassContext;
            _userRepoResolver = userRepoResolver;

            // FAIL-FAST: Stop at the first failure to save DB resources
            ClassLevelCascadeMode = CascadeMode.Stop;

            // RULE 1: Conflict Check
            RuleFor(x => x)
                .Must(UserDoesNotExist)
                .WithErrorCode(ValidationErrorCodes.UserConflict)
                .WithMessage("The Login already Exists !");

            // RULE 2: Specific Data Integrity
            RuleFor(x => x)
                .Custom((user, context) =>
                {
                  //  == true: Since?.Any() returns a bool? (nullable boolean),
                  //  we compare it to true to ensure the list is both not null and not empty.
                    if (user is UserJobSeekerModel seeker && seeker.Skills?.Any() == true)
                    {
                        if (!ValidSkills(seeker))
                        {
                            context.AddFailure(new ValidationFailure(nameof(UserJobSeekerModel.Skills), "Invalid skills provided")
                            {
                                ErrorCode = ValidationErrorCodes.SkillsNotFound
                            });
                        }
                    }
                });
        }

        private bool UserDoesNotExist(UserModel user)
        {
            var userType = user is UserJobSeekerModel ? UserType.JobSeeker : UserType.Hr;
            var repo = _userRepoResolver(userType.ToString());
            return repo.GetUser(user.Login) == null;
        }

        private bool ValidSkills(UserJobSeekerModel seeker)
        {
            var skillIds = seeker.Skills.Select(s => s.Id).ToList();
            var validSkillCount = _dbContext.Skills.Count(s => skillIds.Contains(s.Id));
            return validSkillCount == skillIds.Count;
        }
    }
}