using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
        T GetByProperty(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        List<T> GetAll();

    }
}
