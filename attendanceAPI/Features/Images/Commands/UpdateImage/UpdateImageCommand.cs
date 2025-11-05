using Ardalis.Result;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Images.Commands.UpdateImage
{
    public class UpdateImageCommand : IRequest<Result<Image>>
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
        public UpdateImageCommand(Guid id, IFormFile request)
        {
            Id = id;
            File = request;
        }
    }
}