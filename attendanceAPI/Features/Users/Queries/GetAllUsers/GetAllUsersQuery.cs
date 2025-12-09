using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersResponse>>
    {

    }
}