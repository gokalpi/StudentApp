using AutoMapper;
using StudentApp.V1.Domain.Models;
using StudentApp.V1.Domain.Models.Queries;
using StudentApp.V1.DTO.Request;
using StudentApp.V1.DTO.Response;

namespace StudentApp.V1.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<SaveStudentDto, Student>();

            CreateMap<SaveAddressDto, Address>();

            CreateMap<QueryResult<Student>, QueryResultDto<StudentDto>>();
        }
    }
}