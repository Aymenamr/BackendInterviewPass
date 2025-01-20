using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Processors
{
    public interface IExamProcessor
    {
        void ProcessExam(ExamModel exam);
    }
}
