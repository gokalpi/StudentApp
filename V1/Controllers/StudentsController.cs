using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace StudentApp.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _repository;

        public StudentsController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets a list of all students
        /// </summary>
        /// <returns>List of all students</returns>
        /// <response code="200">The successfully retrieved students.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), Status200OK)]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Gets a specific student's details
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student details</returns>
        /// <response code="200">The student was successfully retrieved.</response>
        /// <response code="404">The student does not exits</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Student), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                throw new ApiException($"Student with id: {id} does not exist.", Status404NotFound);
            }

            return student;
        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Students
        ///     {
        ///       "id": 0,
        ///       "name": "Ibrahim Gokalp",
        ///       "email": "gokalpi@gmail.com",
        ///       "phone": "+90-xxx-xxxxxxx",
        ///       "gender": "Male",
        ///       "bloodGroup": "XX",
        ///       "address": {
        ///         "street": "XXXX",
        ///         "city": "Istanbul",
        ///         "state": "TR",
        ///         "country": "Turkey"
        ///       }
        ///     }
        ///
        /// </remarks>
        /// <param name="student">Student details</param>
        /// <returns>Api response</returns>
        /// <response code="201">Returns the created student</response>
        /// <response code="400">If model state is not valid</response>
        /// <response code="500">If exception occurs during delete</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ApiResponse> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newStudent = await _repository.CreateAsync(student);

                    return new ApiResponse("Student successfully created.", newStudent, Status201Created);
                }
                catch (System.Exception e)
                {
                    throw new ApiException(e);
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Updates a specific student
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Students
        ///     {
        ///       "id": 1,
        ///       "name": "Ibrahim Gokalp",
        ///       "email": "gokalpi@gmail.com",
        ///       "phone": "+90-xxx-xxxxxxx",
        ///       "gender": "Male",
        ///       "bloodGroup": "XX",
        ///       "address": {
        ///         "street": "XXXX",
        ///         "city": "Istanbul",
        ///         "state": "TR",
        ///         "country": "Turkey"
        ///       }
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Student id</param>
        /// <param name="student">Updated student details</param>
        /// <returns>Api response</returns>
        /// <response code="200">Returns true if successfully updated</response>
        /// <response code="400">If model state is not valid</response>
        /// <response code="404">The student does not exits</response>
        /// <response code="500">If exception occurs during delete</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ApiResponse> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                throw new ApiException($"Id {id} with entity id: {student.Id} does not match.", Status400BadRequest);
            }

            if (!await _repository.ExistsAsync(id))
            {
                throw new ApiException($"Student with Id: {id} does not exist.", Status404NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(student);

                    return new ApiResponse($"Student with Id: {student.Id} successfully updated.", true);
                }
                catch (System.Exception e)
                {
                    throw new ApiException(e);
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Deletes a specific student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Api response</returns>
        /// <response code="200">Returns true if successfully deleted</response>
        /// <response code="404">The student does not exits</response>
        /// <response code="500">If exception occurs during delete</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ApiResponse> DeleteStudent(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                throw new ApiException($"Student with Id: {id} does not exist.", Status404NotFound);
            }

            try
            {
                await _repository.DeleteAsync(student);

                return new ApiResponse($"Student with Id: {id} successfully deleted.", true);
            }
            catch (System.Exception e)
            {
                throw new ApiException(e);
            }
        }
    }
}