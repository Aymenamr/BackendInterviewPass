using InterviewPass.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPass.WebApi.Processors.Exam
{
    public interface IExamProcessor
    {
        ExamModel ProcessExam(ExamModel exam);
    }
}
