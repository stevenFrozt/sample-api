using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Features.Users.Commands.UpdateUser;
using attendanceAPI.Models;
using AutoMapper;
using MediatR;

namespace attendanceAPI.Features.Users.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<User>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteUserHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();

            var existingUser = await _context.User!.FindAsync(new object?[] { request.Id }, cancellationToken);

            if (existingUser == null)
                return Result.NotFound();

            _context.User!.Remove(existingUser);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(existingUser);
        }
    }
}