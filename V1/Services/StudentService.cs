using StudentApp.V1.Domain.Repositories;
using StudentApp.V1.Domain.Services;
using StudentApp.V1.Domain.Services.Communication;
using StudentApp.V1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.V1.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> FindByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<StudentResponse> CreateAsync(Student student)
        {
            try
            {
                await _studentRepository.CreateAsync(student);
                await _unitOfWork.SaveChangesAsync();

                return new StudentResponse(student);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when saving the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> UpdateAsync(int id, Student student)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found.");

            existingStudent.Name = student.Name;

            try
            {
                _studentRepository.Update(existingStudent);
                await _unitOfWork.SaveChangesAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when updating the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> DeleteAsync(int id)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
                return new StudentResponse("Student not found.");

            try
            {
                _studentRepository.Delete(existingStudent);
                await _unitOfWork.SaveChangesAsync();

                return new StudentResponse(existingStudent);
            }
            catch (Exception ex)
            {
                return new StudentResponse($"An error occurred when deleting the student: {ex.Message}");
            }
        }
    }
}