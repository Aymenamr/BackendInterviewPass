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
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetByProperty(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetByPropertyWithQueryIncludes(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null);
        List<T> GetAll();

    }
}
