using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors.Result
{
    public interface IResultProcessor
    {
        List<ResultModel> GetAll();
        ResultModel? GetById(string id);
        ResultModel Create(ResultModel model);
        ResultModel? Update(string id, ResultModel model);
        bool Delete(string id);
    }
}
