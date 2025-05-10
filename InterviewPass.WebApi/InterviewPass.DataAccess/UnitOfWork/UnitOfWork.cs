using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories;
using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Exam>  ExamRepo { get; }
        public IGenericRepository<Question> QuestionRepo { get;}
        public IGenericRepository<Possibility> PossibilityRepo { get;}
        //Lubna(Answer)
        public IGenericRepository<Answer> AnswerRepo { get; }


        private readonly DbContext _dbContext;
    
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            ExamRepo = new GenericRepository<Exam>(_dbContext);
            QuestionRepo = new GenericRepository<Question>(_dbContext);
            PossibilityRepo = new GenericRepository<Possibility>(_dbContext);
            //Lubna(Answer)
            AnswerRepo = new GenericRepository<Answer>(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
