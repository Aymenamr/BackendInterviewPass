using InterviewPass.WebApi.Models.Question;

namespace InterviewPass.WebApi.Processors.Question
{
    public interface IQuestionProcessor
    {
        List<QuestionModel> GetAll();
        QuestionModel? GetById(string id);
        QuestionModel Create(QuestionModel model);
        QuestionModel? Update(string id, QuestionModel model);
        bool Delete(string id);
    }
}
