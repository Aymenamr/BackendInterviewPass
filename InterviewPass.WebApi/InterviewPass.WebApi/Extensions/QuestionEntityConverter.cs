using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Extensions
{
    public static class QuestionEntityConverter
    {
        public static Question GetQuestionEntiy(this QuestionModel model, IMapper mapper)
        {
            if (model is MultipleChoiceQuestionModel mcq)
            {
                return mapper.Map<MultipleChoiceQuestion>(mcq);
            }
            else if (model is TrueFalseQuestionModel trueFalseQuestion)
            {
                return mapper.Map<TrueFalseQuestion>(trueFalseQuestion);
            }
            else if (model is PracticalQuestionModel pq)
            {
                return mapper.Map<PracticalQuestion>(pq);
            }
            else if (model is ObjectiveQuestionModel oq)
            {
                return mapper.Map<ObjectiveQuestion>(oq);
            }
            return null;
        }
        public static Question GetQuestionEntiy(this QuestionModel model)
        {
            string qId = Guid.NewGuid().ToString();
            if (model is MultipleChoiceQuestionModel mcq)
            {
                return new MultipleChoiceQuestion
                {
                    Id = qId,
                    Content = mcq.Content,
                    Score = mcq.Score,
                    SkillId = mcq.SkillId,
                    HasSignleChoice = mcq.HasSignleChoice,
                    Possibilities = mcq.Possibilities.Select(p => new Possibility
                    {
                        Id = Guid.NewGuid().ToString(),
                        IsTheCorrectAnswer = p.IsTheCorrectAnswer,
                        Content = p.Content
                    }).ToList()
                };
            }
            else if (model is TrueFalseQuestionModel trueFalseQuestion)
            {
                return new TrueFalseQuestion
                {
                    Id = qId,
                    Content = trueFalseQuestion.Content,
                    SkillId = trueFalseQuestion.SkillId,
                    IsTrue = trueFalseQuestion.IsTrue,
                    Score = trueFalseQuestion.Score
                };
            }
            else if (model is PracticalQuestionModel pq)
            {
                return new PracticalQuestion
                {
                    Id = qId,
                    Content = pq.Content,
                    SkillId = pq.SkillId,
                    Score = pq.Score,
                };
            }
            else if (model is ObjectiveQuestionModel oq)
            {
                return new ObjectiveQuestion
                {
                    Id = qId,
                    Content = oq.Content,
                    SkillId = oq.SkillId,
                    Score = oq.Score,
                };
            }
            return null;
        }
        public static QuestionModel GetQuestionModel(this Question model, IMapper mapper)
        {
            if (model is MultipleChoiceQuestion mcq)
            {
                return mapper.Map<MultipleChoiceQuestionModel>(mcq);
            }
            else if (model is TrueFalseQuestion trueFalseQuestion)
            {
                return mapper.Map<TrueFalseQuestionModel>(trueFalseQuestion);
            }
            else if (model is PracticalQuestion pq)
            {
                return mapper.Map<PracticalQuestionModel>(pq);
            }
            else if (model is ObjectiveQuestion oq)
            {
                return mapper.Map<ObjectiveQuestionModel>(oq);
            }
            return null;
        }
        public static QuestionModel GetQuestionModel(this Question model)
        {
            
            if (model is MultipleChoiceQuestion mcq)
            {
                
                return new MultipleChoiceQuestionModel
                {
                    Id = model.Id,
                    Content = mcq.Content,
                    Score = mcq.Score,
                    SkillId = mcq.SkillId,
                    HasSignleChoice = mcq.HasSignleChoice,
                    Possibilities = mcq.Possibilities.Select(p => new PossibilityModel
                    {
                        Id = p.Id,
                        QuestionId = p.QuestionId,
                        IsTheCorrectAnswer = p.IsTheCorrectAnswer,
                        Content = p.Content
                    }).ToList()
                };
            }
            else if (model is TrueFalseQuestion trueFalseQuestion)
            {
                return new TrueFalseQuestionModel
                {
                    Id = model.Id,
                    Content = trueFalseQuestion.Content,
                    SkillId = trueFalseQuestion.SkillId,
                    IsTrue = trueFalseQuestion.IsTrue,
                    Score = trueFalseQuestion.Score
                };
            }
            else if (model is PracticalQuestion pq)
            {
                return new PracticalQuestionModel
                {
                    Id = model.Id,
                    Content = pq.Content,
                    SkillId = pq.SkillId,
                    Score = pq.Score
                };
            }
            else if (model is ObjectiveQuestion oq)
            {
                return new ObjectiveQuestionModel
                {
                    Id = model.Id,
                    Content = oq.Content,
                    SkillId = oq.SkillId,
                    Score = oq.Score
                };
            }
            return null;
        }
    }
}
