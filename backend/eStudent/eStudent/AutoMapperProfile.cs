using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Course;
using eStudent.DTO.CourseType;
using eStudent.DTO.Request;
using eStudent.DTO.Role;
using eStudent.DTO.Subject;
using eStudent.DTO.User;
using eStudent.Models;
using System.Linq;

namespace eStudent
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<SubjectCreateDto, Subject>();
            CreateMap<SubjectUpdateDto, Subject>();

            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();

            CreateMap<RequestCreateDto, UserCourse>();
            CreateMap<RequestUpdateDto, UserCourse>();

            CreateMap<CourseTypeCreateDto, CourseType>();
            CreateMap<CourseTypeUpdateDto, CourseType>();

            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseUpdateDto, Course>();

            CreateMap<User, StudentGetDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Roles.FirstOrDefault().Role));
        }
    }
}
