using StudentApp.V1.Domain.Services.Communication;
using StudentApp.V1.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.V1.Domain.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> ListAsync();

        Task<Student> FindByIdAsync(int id);

        Task<StudentResponse> CreateAsync(Student student);

        Task<StudentResponse> UpdateAsync(int id, Student student);

        Task<StudentResponse> DeleteAsync(int id);
    }
}