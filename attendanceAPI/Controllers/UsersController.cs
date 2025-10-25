using Ardalis.Result;
using attendanceAPI.Features.Users.Commands.CreateUser;
using attendanceAPI.Features.Users.Commands.DeleteUser;
using attendanceAPI.Features.Users.Commands.UpdateUser;
using attendanceAPI.Features.Users.Queries.GetAllUsers;
using attendanceAPI.Features.Users.Queries.GetUserById;
using attendanceAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace attendanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {

            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };
        }


        [HttpPost]
        public async Task<ActionResult<User>> AddUser(CreateUserRequest request)
        {

            var result = await _mediator.Send(new CreateUserCommand(request));
            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                ResultStatus.Created => CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };


        }



        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, UpdateUserRequest request)
        {

            var result = await _mediator.Send(new UpdateUserCommand(id, request));
            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                ResultStatus.Created => CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {


            var result = await _mediator.Send(new DeleteUserCommand(id));
            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                ResultStatus.Created => CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };

        }
    }

}
