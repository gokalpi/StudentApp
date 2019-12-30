using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StudentDbContext _context;

        public Repository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            AttachEntity(entity);

            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            AttachEntity(entity);

            _context.Set<T>().Remove(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return null != await GetByIdAsync(id);
        }

        private void AttachEntity(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
        }
    }
}