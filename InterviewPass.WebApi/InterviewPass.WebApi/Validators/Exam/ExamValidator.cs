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
using InterviewPass.WebApi.Models.ResponseResult;

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

    public ApiResponse Validate(ExamModel exam)
    {
        // Validate Author Exists
        if (_hrRepository.GetUserById(exam.Author) == null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Author not found"
            };
        }

        // Validate Exam Name Unique
        if (_examRepository.GetByProperty(e => e.Name == exam.Name) != null)
        {
            return new ErrorResponse
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = "An exam with the same name already exists"
            };
        }

        // Validate Questions
        if (exam.Questions != null && exam.Questions.Any())
        {
            if (exam.Questions.Count != exam.NbrOfQuestion)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Number of questions does not match"
                };
            }

            foreach (var q in exam.Questions)
            {
                // Skill exists?
                if (_skillRepository.GetByProperty(s => s.Id == q.SkillId) == null)
                {
                    return new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"Skill '{q.SkillId}' not found"
                    };
                }

                // Check Multiple choice
                if (q is MultipleChoiceQuestionModel mcq)
                {
                    if (mcq.Possibilities == null || mcq.Possibilities.Count == 0)
                    {
                        return new ErrorResponse
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Message = "Multiple choice question must have at least one possibility"
                        };
                    }
                }
            }
        }

        return new SuccessResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Message = "Validation passed"
        };
    }

}
