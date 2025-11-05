using Ardalis.Result;
using attendanceAPI.Features.Images.Commands.CreateImage;
using attendanceAPI.Features.Images.Commands.DeleteImage;
using attendanceAPI.Features.Images.Queries.GetAllImage;
using attendanceAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace attendanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Image>>> GetAllImage()
        {
            var images = await _mediator.Send(new GetAllImageQuery());
            return Ok(images);
        }


        [HttpPost("upload")]
        public async Task<ActionResult<Image>> AddImage(IFormFile request)
        {

            var result = await _mediator.Send(new CreateImageCommand(request));

            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                // ResultStatus.Created => CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Image>> DeleteImage(Guid id)
        {

            var result = await _mediator.Send(new DeleteImageCommand(id));
            return result.Status switch
            {
                ResultStatus.Ok => Ok(result.Value),
                ResultStatus.NotFound => NotFound(result.Errors),
                ResultStatus.Invalid => BadRequest(result.ValidationErrors),
                ResultStatus.Error => StatusCode(500, result.Errors),
                _ => BadRequest(result.Errors)
            };


        }


    }

}
