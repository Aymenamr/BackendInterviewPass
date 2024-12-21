using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        InterviewPassContext _dbContext;
        public QuestionRepository(DbContext dbContext)
        {
            _dbContext = dbContext as InterviewPassContext;
        }
        public List<Question> GetAllQuestions()
        {
           return _dbContext.Questions.ToList();
        }
    }
}
