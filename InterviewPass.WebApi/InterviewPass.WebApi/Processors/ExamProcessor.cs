using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Models;
using SQLitePCL;

namespace InterviewPass.WebApi.Processors
{
    public class ExamProcessor : IExamProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExamProcessor(UnitOfWork unitOfWork,IMapper mapper)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void ProcessExam(ExamModel exam)
        {
            if (exam.Questions.Any())
            {
                if (exam.Questions.Count != exam.NbrOfQuestion)
                {
                    return;
                   // return BadRequest("The number of questions is not equal to the number of questions introduced");
                }
                exam.MaxScore = exam.Questions.Sum(q => q.Score);
            }

            Exam examEntity = _mapper.Map<Exam>(exam);
            
            foreach (var question in exam.Questions)
            {
                //TODO : Map Question according to type
                Question questionEntity = _mapper.Map<Question>(question);
                _unitOfWork.QuestionRepo.Add(questionEntity);
            }

            //TODO Get the IDs of the added question

            //TODO ADD the Exam with their ExamQuestion row

            _unitOfWork.Save();
        }
    }
}
