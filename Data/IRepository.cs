using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentApp.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        Task<IList<T>> GetAllAsync();

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        T Find(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

        Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        IList<T> Filter(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        int Count();

        Task<int> CountAsync();

        T Create(T entity);

        Task<T> CreateAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        bool Delete(T entity);

        Task<bool> DeleteAsync(T entity);

        bool Exists(object id);

        Task<bool> ExistsAsync(object id);

        bool Exists(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}