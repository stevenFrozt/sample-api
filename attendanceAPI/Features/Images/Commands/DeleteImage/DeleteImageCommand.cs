using Ardalis.Result;
using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest<Result<Image>>
    {
        public Guid Id { get; set; }
        public DeleteImageCommand(Guid request)
        {
            Id = request;
        }
    }
}