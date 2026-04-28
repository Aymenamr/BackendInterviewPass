using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;
using InterviewPass.WebApi.Models.ResponseResult;

namespace InterviewPass.WebApi.Validators.Result
{
    public class ResultValidator : IResultValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserJobSeeker> _userRepository;

        public ResultValidator(IUnitOfWork unitOfWork, IGenericRepository<UserJobSeeker> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public ApiResponse ValidateForCreate(ResultModel result)
        {
            return Validate(result);
        }

        public ApiResponse ValidateForUpdate(string id, ResultModel result)
        {
            var existingResult = _unitOfWork.ResultRepo.GetByProperty(r => r.Id == id);
            if (existingResult == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Result with ID {id} not found"
                };
            }

            return Validate(result);
        }

        private ApiResponse Validate(ResultModel result)
        {
            if (result == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Result cannot be null"
                };
            }

            if (!string.IsNullOrWhiteSpace(result.UserId) &&
                _userRepository.GetByProperty(user => user.Id == result.UserId) == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "User ID does not exist"
                };
            }

            if (!string.IsNullOrWhiteSpace(result.ExamId) &&
                _unitOfWork.ExamRepo.GetByProperty(exam => exam.Id == result.ExamId) == null)
            {
                return new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Exam ID does not exist"
                };
            }

            return new SuccessResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Validation passed"
            };
        }
    }
}
