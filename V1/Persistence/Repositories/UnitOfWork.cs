using StudentApp.V1.Domain.Repositories;
using StudentApp.V1.Persistence.Contexts;
using System.Threading.Tasks;

namespace StudentApp.V1.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _context;

        public UnitOfWork(StudentDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}