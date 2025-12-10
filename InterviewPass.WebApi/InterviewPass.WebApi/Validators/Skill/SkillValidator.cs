using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.Infrastructure.Exceptions;
using InterviewPass.WebApi.Models;
using System.ComponentModel.DataAnnotations;
using InterviewPass.WebApi.Validators.Skill;
using System.Xml.Linq;

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

    public void Validate(SkillModel skill)
    {
        if (skill == null)
            throw new ValidationException("Skill cannot be null.");

        // Check name
        if (string.IsNullOrWhiteSpace(skill.Name))
            throw new ValidationException("Skill name cannot be empty.");

        // Check field exists
        var field = _fieldRepository.GetByProperty(f => f.Id == skill.FieldId);
        if (field == null)
            throw new NotFoundException("Field not found.");

        // Check skill name uniqueness
        var existing = _skillRepository.GetByProperty(s => s.Name == skill.Name);
        if (existing != null)
            throw new ValidationException("Skill name already exists.");
    }
}
