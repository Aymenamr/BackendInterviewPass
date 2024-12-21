using InterviewPass.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        List<Question> GetAllQuestions();
    }
}
