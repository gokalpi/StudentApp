using StudentApp.V1.Domain.Models;
using StudentApp.V1.Domain.Repositories;
using StudentApp.V1.Persistence.Contexts;

namespace StudentApp.V1.Persistence.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(StudentDbContext context) : base(context)
        {
        }
    }
}