using StudentApp.V1.Domain.Models;

namespace StudentApp.V1.Domain.Services.Communication
{
    public class StudentResponse : BaseResponse<Student>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="student">Saved student.</param>
        /// <returns>Response.</returns>
        public StudentResponse(Student student) : base(student)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public StudentResponse(string message) : base(message)
        { }
    }
}