using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);

        Task<T> CreateAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);

        Task<bool> ExistsAsync(object id);
    }
}