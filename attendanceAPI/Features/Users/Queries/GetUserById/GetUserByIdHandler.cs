using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<User>>
    {
        private readonly AppDbContext _context;

        public GetUserByIdHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) return Result.Invalid();
            var user = await _context.User!.FindAsync(new object[] { request.Id }, cancellationToken);
            if (user == null) return Result.NotFound();
            return Result.Success(user);

        }
    }
}