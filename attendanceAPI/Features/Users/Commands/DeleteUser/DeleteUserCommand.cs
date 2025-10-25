using Ardalis.Result;
using attendanceAPI.Features.Users.Commands.CreateUser;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result<User>>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;

        }
    }
}