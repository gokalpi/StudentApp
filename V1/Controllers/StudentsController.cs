using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentApp.Helpers.Extensions;
using StudentApp.V1.Domain.Services;
using StudentApp.V1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace StudentApp.V1.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService service, ILogger<StudentsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        /// <summary>
        /// Gets a list of all students
        /// </summary>
        /// <returns>List of all students</returns>
        /// <response code="200">The successfully retrieved students.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), Status200OK)]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            _logger.LogInformation("Getting all students");

            return await _service.ListAsync();
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
            _logger.LogInformation($"Getting student with id {id}");

            var student = await _service.FindByIdAsync(id);

            if (student == null)
            {
                _logger.LogError($"Student with id {id} does not exist");
                return NotFound($"Student with id: {id} does not exist.");
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
        /// <returns>Created student</returns>
        /// <response code="200">Returns the created student</response>
        /// <response code="400">If model state is not valid or an error occured during operation</response>
        [HttpPost]
        [ProducesResponseType(typeof(Student), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _logger.LogInformation($"Creating student {student}");

            if (ModelState.IsValid)
            {
                var result = await _service.CreateAsync(student);
                if (result.Success)
                {
                    _logger.LogInformation("Student successfully created.");
                    return Ok(result.Resource);
                }
                else
                {
                    _logger.LogError("Error in creating student\r\n{0}", result.Message);
                    return BadRequest(result.Message);
                }
            }
            else
            {
                _logger.LogError("Model is not valid");
                return BadRequest(ModelState.GetErrorMessages());
            }
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
        /// <returns>Updated student</returns>
        /// <response code="200">Returns the updated student</response>
        /// <response code="400">If model state is not valid or an error occured during operation</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Student), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
        {
            _logger.LogInformation($"Updating student {student}");

            if (ModelState.IsValid)
            {
                var result = await _service.UpdateAsync(id, student);
                if (result.Success)
                {
                    _logger.LogInformation("Student successfully updated.");
                    return Ok(result.Resource);
                }
                else
                {
                    _logger.LogError("Error in updating student\r\n{0}", result.Message);
                    return BadRequest(result.Message);
                }
            }
            else
            {
                _logger.LogError("Model is not valid");
                return BadRequest(ModelState.GetErrorMessages());
            }
        }

        /// <summary>
        /// Deletes a specific student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Deleted student</returns>
        /// <response code="200">Returns the deleted student</response>
        /// <response code="400">An error occured during operation</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Student), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            _logger.LogInformation($"Deleting student with id {id}");

            var result = await _service.DeleteAsync(id);
            if (result.Success)
            {
                _logger.LogInformation("Student successfully deleted.");
                return Ok(result.Resource);
            }
            else
            {
                _logger.LogError("Error in deleting student\r\n{0}", result.Message);
                return BadRequest(result.Message);
            }
        }
    }
}