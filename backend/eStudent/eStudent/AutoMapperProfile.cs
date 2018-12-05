using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Course;
using eStudent.DTO.CourseType;
using eStudent.DTO.Request;
using eStudent.DTO.Role;
using eStudent.DTO.Subject;
using eStudent.DTO.User;
using eStudent.Models;

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

            CreateMap<RequestCreateDto, Request>();
            CreateMap<RequestUpdateDto, Request>();

            CreateMap<CourseTypeCreateDto, CourseType>();
            CreateMap<CourseTypeUpdateDto, CourseType>();

            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseUpdateDto, Course>();
        }
    }
}
