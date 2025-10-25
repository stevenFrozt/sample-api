using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Features.Users.Commands.UpdateUser;
using attendanceAPI.Models;
using AutoMapper;
using MediatR;

namespace attendanceAPI.Features.Users.Commands.CreateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateUserHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();

            var existingUser = await _context.User!.FindAsync(new object?[] { request.Id }, cancellationToken);

            if (existingUser == null)
                return Result.NotFound();

            _mapper.Map(request, existingUser);


            _context.User!.Update(existingUser);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(existingUser);
        }
    }
}