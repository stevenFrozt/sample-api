using attendanceAPI.Data;
using attendanceAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace attendanceAPI.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersResponse>>
    {
        private readonly AppDbContext _context;

        public GetAllUsersHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.User!
            .Include(u => u.Image)  // ← Add this to load Image data
            .ToListAsync(cancellationToken);

            return new List<GetAllUsersResponse>(users.Select(user => new GetAllUsersResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                Image = user.Image
            }));
        }
    }
}