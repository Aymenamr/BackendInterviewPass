using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.ResponseResult;
using QuestionEntity = InterviewPass.DataAccess.Entities.Question;

namespace InterviewPass.WebApi.Validators.Question
{
    public class QuestionValidator : IQuestionValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApiResponse ValidateForCreate(QuestionModel question)
        {
            return Validate(question);
        }

        public ApiResponse ValidateForUpdate(string id, QuestionModel question)
        {
            var existingQuestion = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id);
            if (existingQuestion == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Question not found"
                };
            }

            if (!IsMatchingQuestionType(existingQuestion, question))
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Changing the question type is not supported"
                };
            }

            return Validate(question);
        }

        private ApiResponse Validate(QuestionModel question)
        {
            if (question == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Question cannot be null"
                };
            }

            var skill = _unitOfWork.SkillRepo.GetByProperty(existingSkill => existingSkill.Id == question.SkillId);
            if (skill == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Skill not found"
                };
            }

            if (question is MultipleChoiceQuestionModel multipleChoiceQuestion &&
                (multipleChoiceQuestion.Possibilities == null || !multipleChoiceQuestion.Possibilities.Any()))
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Multiple choice question must have at least one possibility"
                };
            }

            return new SuccessResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Validation passed"
            };
        }

        private static bool IsMatchingQuestionType(QuestionEntity existingQuestion, QuestionModel incomingQuestion)
        {
            return existingQuestion switch
            {
                MultipleChoiceQuestion => incomingQuestion is MultipleChoiceQuestionModel,
                TrueFalseQuestion => incomingQuestion is TrueFalseQuestionModel,
                PracticalQuestion => incomingQuestion is PracticalQuestionModel,
                ObjectiveQuestion => incomingQuestion is ObjectiveQuestionModel,
                _ => false
            };
        }
    }
}
