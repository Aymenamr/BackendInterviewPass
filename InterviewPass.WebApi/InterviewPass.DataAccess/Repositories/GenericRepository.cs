using InterviewPass.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace InterviewPass.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            AssignGuidIfNeeded(entity);
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetByProperty(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Apply the Include expressions (if any)
            foreach (var include in includes)
            {
                query = query.Include(include); // Apply eager loading
            }

            return query.FirstOrDefault(predicate); // Apply the condition and return the entity
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        private void AssignGuidIfNeeded(T entity)
        {
            var type = typeof(T);
            var idProperty = type.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

            if (idProperty != null && idProperty.PropertyType == typeof(string))
            {
                idProperty.SetValue(entity, Guid.NewGuid().ToString());
            }
        }
    }
}
