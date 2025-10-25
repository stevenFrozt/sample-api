using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Models;
using AutoMapper;
using MediatR;

namespace attendanceAPI.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();

            var newUser = _mapper.Map<User>(request);

            await _context.User!.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Created(newUser);
        }
    }
}