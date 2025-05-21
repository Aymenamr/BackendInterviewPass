using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace InterviewPass.WebApi.Processors
{
    public class ResultProcessor : IResultProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResultProcessor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResultModel ProcessResult(ResultModel Result)
        {

            Result ResultEntity = _mapper.Map<Result>(Result);
            List<string> idQuestions = new List<string>();

            //Add the Questions with their possibilities
            foreach (var question in Result.Questions)
            {
                Question questionEntity = question.GetQuestionEntiy(_mapper);
                idQuestions.Add(_unitOfWork.QuestionRepo.Add(questionEntity).Id);
            }

            //Add the Results
            string idResult = _unitOfWork.ResultRepo.Add(ResultEntity).Id;

            //Add to the associative table
            foreach (var idQuestion in idQuestions)
            {
                ResultEntity.QuestionResults.Add(
                       new QuestionResults()
                       {
                           IdQuestion = idQuestion,
                           IdResult = idResult
                       });
            }
            Result.Id = idResult;
            _unitOfWork.Save();
            return Result;
        }
        
        void IResultProcessor.ProcessResult(ResultModel result)
        {
            throw new NotImplementedException();
        }
    }
}
