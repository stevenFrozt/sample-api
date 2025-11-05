using attendanceAPI.Models;
using MediatR;

namespace attendanceAPI.Features.Images.Queries.GetAllImage
{
    public class GetAllImageQuery : IRequest<List<Image>>
    {

    }
}