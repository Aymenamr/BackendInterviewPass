using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.DataAccess.UnitOfWork;
using InterviewPass.WebApi.Extensions;
using InterviewPass.WebApi.Models.Question;
using QuestionEntity = InterviewPass.DataAccess.Entities.Question;

namespace InterviewPass.WebApi.Processors.Question
{
    public class QuestionProcessor : IQuestionProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionProcessor(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<QuestionModel> GetAll()
        {
            var questions = _unitOfWork.QuestionRepo.GetAll();
            return questions.Select(question => question.GetQuestionModel(_mapper)).ToList();
        }

        public QuestionModel? GetById(string id)
        {
            var question = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id, q => q.Possibilities);
            return question?.GetQuestionModel(_mapper);
        }

        public QuestionModel Create(QuestionModel model)
        {
            var questionEntity = model.GetQuestionEntiy(_mapper);
            var createdQuestion = _unitOfWork.QuestionRepo.Add(questionEntity);
            _unitOfWork.Save();

            return createdQuestion.GetQuestionModel(_mapper);
        }

        public QuestionModel? Update(string id, QuestionModel model)
        {
            var existingQuestion = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id, q => q.Possibilities);
            if (existingQuestion == null)
            {
                return null;
            }

            MapQuestion(model, existingQuestion);

            _unitOfWork.QuestionRepo.Update(existingQuestion);
            _unitOfWork.Save();

            return existingQuestion.GetQuestionModel(_mapper);
        }

        public bool Delete(string id)
        {
            var question = _unitOfWork.QuestionRepo.GetByProperty(q => q.Id == id);
            if (question == null)
            {
                return false;
            }

            _unitOfWork.QuestionRepo.Delete(question);
            _unitOfWork.Save();
            return true;
        }

        private static void MapQuestion(QuestionModel source, QuestionEntity destination)
        {
            destination.Content = source.Content;
            destination.Score = source.Score;
            destination.SkillId = source.SkillId;

            switch (destination)
            {
                case MultipleChoiceQuestion multipleChoice when source is MultipleChoiceQuestionModel multipleChoiceModel:
                    multipleChoice.HasSignleChoice = multipleChoiceModel.HasSignleChoice;
                    multipleChoice.Possibilities.Clear();
                    foreach (var possibility in multipleChoiceModel.Possibilities)
                    {
                        multipleChoice.Possibilities.Add(new Possibility
                        {
                            Id = string.IsNullOrWhiteSpace(possibility.Id) ? Guid.NewGuid().ToString() : possibility.Id,
                            Content = possibility.Content,
                            IsTheCorrectAnswer = possibility.IsTheCorrectAnswer,
                            QuestionId = destination.Id
                        });
                    }
                    break;

                case TrueFalseQuestion trueFalseQuestion when source is TrueFalseQuestionModel trueFalseModel:
                    trueFalseQuestion.IsTrue = trueFalseModel.IsTrue;
                    break;
            }
        }
    }
}
