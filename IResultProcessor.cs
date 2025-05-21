using InterviewPass.WebApi.Models;

namespace InterviewPass.WebApi.Processors
{
    public interface IResultProcessor
    {
        void ProcessResult(ResultModel result);
    }
}
