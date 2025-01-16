using InterviewPass.DataAccess.Entities;
using InterviewPass.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.UnitOfWork
{
    public class UnitOfWork
    {
        private IGenericRepository<Exam>  _examRepo { get; set; }
        private IGenericRepository<Question> _questionRepo { get; set; }

        public UnitOfWork()
        {
        
        }

    }
}
