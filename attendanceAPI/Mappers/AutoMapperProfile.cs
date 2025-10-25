using attendanceAPI.Features.Users.Commands.CreateUser;
using attendanceAPI.Features.Users.Commands.UpdateUser;
using attendanceAPI.Models;
using AutoMapper;

namespace attendanceAPI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map from DTO -> Model
            CreateMap<CreateUserRequest, User>();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();


            // Map from Model -> DTO (optional)
            CreateMap<User, CreateUserRequest>();
        }
    }
}