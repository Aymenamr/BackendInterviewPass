using AutoMapper;
using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Entities.Questions;
using InterviewPass.WebApi.Models.Question;
using InterviewPass.WebApi.Models.User;

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
    }
}
