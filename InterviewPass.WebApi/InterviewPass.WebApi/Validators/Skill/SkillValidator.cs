using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.Infrastructure.Exceptions;
using InterviewPass.WebApi.Models;
using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Validators.Skill;
using System.Xml.Linq;
using InterviewPass.WebApi.Models.ResponseResult;

public class SkillValidator : ISkillValidator
{
    private readonly IGenericRepository<Field> _fieldRepository;
    private readonly IGenericRepository<Skill> _skillRepository;

    public SkillValidator(
        IGenericRepository<Field> fieldRepository,
        IGenericRepository<Skill> skillRepository)
    {
        _fieldRepository = fieldRepository;
        _skillRepository = skillRepository;
    }

    public ApiResponse Validate(SkillModel skill)
    {
        if (skill == null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Skill cannot be null"
            };
        }

        // Check name
        if (string.IsNullOrWhiteSpace(skill.Name))
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Skill name cannot be empty"
            };
        }

        // Check field exists
        var field = _fieldRepository.GetByProperty(f => f.Id == skill.FieldId);
        if (field == null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Field not found"
            };
        }

        // Check skill name uniqueness
        var existing = _skillRepository.GetByProperty(s => s.Name == skill.Name);
        if (existing != null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Skill name already exists"
            };
        }

        return new SuccessResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Validation passed"
        };
    }

}
