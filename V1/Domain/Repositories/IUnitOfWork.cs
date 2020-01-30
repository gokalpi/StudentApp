using System.Threading.Tasks;

namespace StudentApp.V1.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}