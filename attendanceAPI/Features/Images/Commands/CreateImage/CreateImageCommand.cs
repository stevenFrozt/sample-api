using Ardalis.Result;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Images.Commands.CreateImage
{
    public class CreateImageCommand : IRequest<Result<Image>>
    {

        public IFormFile File { get; set; }

        public CreateImageCommand(IFormFile request)
        {
            File = request;
        }
    }
}