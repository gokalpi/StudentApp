using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentApp.Helpers.Extensions;
using StudentApp.V1.Domain.Models;
using StudentApp.V1.Domain.Services;
using StudentApp.V1.DTO.Request;
using StudentApp.V1.DTO.Response;
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
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService service, IMapper mapper, ILogger<StudentsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a list of all students
        /// </summary>
        /// <returns>List of all students</returns>
        /// <response code="200">The successfully retrieved students.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ListQueryResultDto<StudentDto>), Status200OK)]
        public async Task<ListQueryResultDto<StudentDto>> GetStudents()
        {
            _logger.LogInformation("Getting all students");

            var students = await _service.ListAsync();
            var dtos = _mapper.Map<IList<StudentDto>>(students);
            return new ListQueryResultDto<StudentDto>(dtos);
        }

        /// <summary>
        /// Gets a specific student's details
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student details</returns>
        /// <response code="200">The student was successfully retrieved.</response>
        /// <response code="404">The student does not exits</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status200OK)]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status404NotFound)]
        public async Task<ActionResult<QueryResultDto<StudentDto>>> GetStudent(int id)
        {
            _logger.LogInformation($"Getting student with id {id}");

            var student = await _service.FindByIdAsync(id);

            if (student == null)
            {
                _logger.LogError($"Student with id {id} not found.");
                return NotFound(new QueryResultDto<StudentDto>($"Student with id {id} not found."));
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(new QueryResultDto<StudentDto>(studentDto));
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
        /// <param name="dto">Student details</param>
        /// <returns>Created student</returns>
        /// <response code="200">Returns the created student</response>
        /// <response code="400">If model state is not valid or an error occured during operation</response>
        [HttpPost]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status200OK)]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status400BadRequest)]
        public async Task<ActionResult<QueryResultDto<StudentDto>>> CreateStudent([FromBody] SaveStudentDto dto)
        {
            _logger.LogInformation($"Creating student {dto}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Model is not valid");
                return BadRequest(new QueryResultDto<StudentDto>(ModelState.GetErrorMessages()));
            }

            var student = _mapper.Map<Student>(dto);
            var result = await _service.CreateAsync(student);

            if (result.Success)
            {
                _logger.LogInformation("Student successfully created.");

                var studentDto = _mapper.Map<StudentDto>(result.Resource);
                return Ok(new QueryResultDto<StudentDto>(studentDto));
            }
            else
            {
                _logger.LogError("Error in creating student\r\n{0}", result.Message);

                return BadRequest(new QueryResultDto<StudentDto>(result.Message));
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
        /// <param name="dto">Updated student details</param>
        /// <returns>Updated student</returns>
        /// <response code="200">Returns the updated student</response>
        /// <response code="400">If model state is not valid or an error occured during operation</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status200OK)]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status400BadRequest)]
        public async Task<ActionResult<QueryResultDto<StudentDto>>> UpdateStudent(int id, SaveStudentDto dto)
        {
            _logger.LogInformation($"Updating student {dto}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Model is not valid");
                return BadRequest(ModelState.GetErrorMessages());
            }

            var student = _mapper.Map<SaveStudentDto, Student>(dto);
            student.Id = id;
            var result = await _service.UpdateAsync(id, student);

            if (result.Success)
            {
                _logger.LogInformation("Student successfully updated.");

                var studentDto = _mapper.Map<StudentDto>(result.Resource);
                return Ok(new QueryResultDto<StudentDto>(studentDto));
            }
            else
            {
                _logger.LogError("Error in updating student\r\n{0}", result.Message);
                return BadRequest(new QueryResultDto<StudentDto>(result.Message));
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
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status200OK)]
        [ProducesResponseType(typeof(QueryResultDto<StudentDto>), Status400BadRequest)]
        public async Task<ActionResult<QueryResultDto<StudentDto>>> DeleteStudent(int id)
        {
            _logger.LogInformation($"Deleting student with id {id}");

            var result = await _service.DeleteAsync(id);
            if (result.Success)
            {
                _logger.LogInformation("Student successfully deleted.");

                var studentDto = _mapper.Map<StudentDto>(result.Resource);
                return Ok(new QueryResultDto<StudentDto>(studentDto));
            }
            else
            {
                _logger.LogError("Error in deleting student\r\n{0}", result.Message);
                return BadRequest(new QueryResultDto<StudentDto>(result.Message));
            }
        }
    }
}