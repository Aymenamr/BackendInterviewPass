using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {   
        IGenericRepository<Exam> ExamRepo { get; }
        IGenericRepository<Possibility> PossibilityRepo { get; }
        IGenericRepository<Question> QuestionRepo { get; }
        //Lubna(Answer)
        IGenericRepository<Answer> AnswerRepo { get; }
        void Save();
    }
}
