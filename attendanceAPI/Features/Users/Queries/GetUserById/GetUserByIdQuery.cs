using Ardalis.Result;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Result<User>>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}