using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.V1.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentApp.V1.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StudentDbContext _context;

        public Repository(StudentDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().AsEnumerable();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable();
        }

        public async Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public IList<T> Filter(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Update(T entity)
        {
            AttachEntity(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            AttachEntity(entity);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public bool Delete(T entity)
        {
            AttachEntity(entity);

            _context.Set<T>().Remove(entity);

            return 1 == _context.SaveChanges();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            AttachEntity(entity);

            _context.Set<T>().Remove(entity);

            return 1 == await _context.SaveChangesAsync();
        }

        public bool Exists(object id)
        {
            return null != GetById(id);
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return null != await GetByIdAsync(id);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
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