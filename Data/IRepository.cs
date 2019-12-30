using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Data
{
    public interface IRepository<T> where T : class
    {
        bool Exists(object id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<T> SaveAsync(T entity);
    }
}