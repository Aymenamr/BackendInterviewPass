using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.WebApi.Enums;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Validators.user;

public class UserValidator : IUserValidator
{
    private readonly IGenericRepository<Skill> _skillRepository;
    private readonly Func<string, IUserRepository> _userRepoResolver;


    public UserValidator(
        IGenericRepository<Skill> skillRepository,
         Func<string, IUserRepository> userRepoResolver)
    {
        _skillRepository = skillRepository;
        _userRepoResolver = userRepoResolver;
    }

    public ApiResponse Validate(UserModel user)
    {
        // 1️ Null check
        if (user == null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "User cannot be null"
            };
        }

        // 2️ Determine user type
        UserType userType = user is UserJobSeekerModel
            ? UserType.JobSeeker
            : UserType.Hr;

        // 3️  Check user existence (LOGIN)
        var existingUser = _userRepoResolver(userType.ToString())
            .GetUser(user.Login);

        if (existingUser != null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = "The Login already exists"
            };
        }

        // 4️  Validate JobSeeker skills
        if (user is UserJobSeekerModel jobSeeker)
        {
            if (jobSeeker.Skills == null || jobSeeker.Skills.Count == 0)
            {
                return new SuccessResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "No skills provided"
                };
            }

            var skillIds = jobSeeker.Skills.Select(s => s.Id).ToList();

            var existingSkillIds = _skillRepository
                .GetAll(s => skillIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToHashSet();

            var missingSkill = jobSeeker.Skills
                .FirstOrDefault(s => !existingSkillIds.Contains(s.Id));

            if (missingSkill != null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Skill '{missingSkill.Name}' not found"
                };
            }
        }

        // 5️⃣ All validations passed
        return new SuccessResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "User validation passed"
        };
    }
}
