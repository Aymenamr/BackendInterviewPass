using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Services;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.Infrastructure.Exceptions;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.User;
using InterviewPass.WebApi.Validators.Exam;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

public class ExamValidator : IExamValidator
{
    private readonly HrRepository _hrRepository;
    private readonly IGenericRepository<Skill> _skillRepository;
    private readonly IGenericRepository<Exam> _examRepository;

    public ExamValidator(
        HrRepository hrRepository,
        IGenericRepository<Skill> skillRepository,
        IGenericRepository<Exam> examRepository)
    {
        _hrRepository = hrRepository;
        _skillRepository = skillRepository;
        _examRepository = examRepository;
    }

    public void Validate(ExamModel exam)
    {
        // Validate Author Exists
        if (_hrRepository.GetUserById(exam.Author) == null)
            throw new NotFoundException("Author not found.");

        // Validate Exam Name Unique
        if (_examRepository.GetByProperty(e => e.Name == exam.Name) != null)
            throw new ValidationException("The exam name already exists.");

        // Validate Questions
        if (exam.Questions != null && exam.Questions.Any())
        {
            if (exam.Questions.Count != exam.NbrOfQuestion)
                throw new ValidationException("Number of questions does not match.");

            foreach (var q in exam.Questions)
            {
                // Skill exists?
                if (_skillRepository.GetByProperty(s => s.Id == q.SkillId) == null)
                    throw new NotFoundException($"Skill '{q.SkillId}' not found.");

                // Check Multiple choice
                if (q is MultipleChoiceQuestionModel mcq)
                {
                    if (mcq.Possibilities == null || mcq.Possibilities.Count == 0)
                        throw new ValidationException("Multiple choice question must have at least one possibility.");
                }
            }

            // Calculate Max Score
            exam.MaxScore = exam.Questions.Sum(q => q.Score);
        }
    }
}
