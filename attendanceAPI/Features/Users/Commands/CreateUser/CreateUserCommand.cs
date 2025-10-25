using Ardalis.Result;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result<User>>
    {

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public CreateUserCommand(CreateUserRequest request)
        {

            Username = request.Username ?? string.Empty;
            Password = request.Password ?? string.Empty;
            FirstName = request.FirstName ?? string.Empty;
            LastName = request.LastName ?? string.Empty;
            Email = request.Email ?? string.Empty;
            Gender = request.Gender ?? string.Empty;
            BirthDate = request.BirthDate;
        }
    }
}