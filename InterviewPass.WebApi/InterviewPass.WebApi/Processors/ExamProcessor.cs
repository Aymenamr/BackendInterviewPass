
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
    public class ExamProcessor : IExamProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExamProcessor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ExamModel ProcessExam(ExamModel exam)
        {

            Exam examEntity = _mapper.Map<Exam>(exam);
            List<string> idQuestions = new List<string>();

            //Add the Questions with their possibilities
            foreach (var question in exam.Questions)
            {
                Question questionEntity = question.GetQuestionEntiy(_mapper);
                idQuestions.Add(_unitOfWork.QuestionRepo.Add(questionEntity).Id);
            }

            //Add the exams
            string idExam = _unitOfWork.ExamRepo.Add(examEntity).Id;

            //Add to the associative table
            foreach (var idQuestion in idQuestions)
            {
                examEntity.QuestionExams.Add(
                       new QuestionExam()
                       {
                           IdQuestion = idQuestion,
                           IdExam = idExam
                       });
            }
            exam.Id = idExam;
            _unitOfWork.Save();
            return exam;
        }
    }
}
