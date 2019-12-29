using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
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
        /// <returns>List of students</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return Ok(await _repository.GetAllAsync());
        }

        /// <summary>
        /// Gets a specific student's details
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student details</returns>
        /// <response code="400">If the student id is not a valid number</response>
        /// <response code="404">If the student is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
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
        /// <returns></returns>
        /// <response code="204">Returns no content if successful</response>
        /// <response code="400">If the student id is different than id in content</response>
        /// <response code="404">If the student is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(student);
                var updatedStudent = await _repository.SaveAsync(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a new student
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
        /// <returns>Created student</returns>
        /// <response code="201">Returns the created student</response>
        /// <response code="400">If the student is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            _repository.Add(student);
            var addedStudent = await _repository.SaveAsync(student);

            return CreatedAtAction("GetStudent", new { id = addedStudent.Id }, addedStudent);
        }

        /// <summary>
        /// Deletes a specific student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Deleted student</returns>
        /// <response code="200">Returns the deleted student</response>
        /// <response code="400">If the id is null</response>
        /// <response code="404">If the student is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _repository.Delete(student);
            var deletedStudent = await _repository.SaveAsync(student);

            return deletedStudent;
        }
    }
}