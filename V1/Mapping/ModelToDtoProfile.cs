using AutoMapper;
using StudentApp.V1.Domain.Models;
using StudentApp.V1.Domain.Models.Queries;
using StudentApp.V1.DTO.Response;

namespace StudentApp.V1.Mapping
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<Address, AddressDto>();

            CreateMap<QueryResult<Student>, QueryResultDto<StudentDto>>();
        }
    }
}