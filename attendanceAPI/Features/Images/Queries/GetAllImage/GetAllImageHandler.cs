using attendanceAPI.Data;
using attendanceAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace attendanceAPI.Features.Images.Queries.GetAllImage
{
    public class GetAllImageHandler : IRequestHandler<GetAllImageQuery, List<Image>>
    {
        private readonly AppDbContext _context;

        public GetAllImageHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> Handle(GetAllImageQuery request, CancellationToken cancellationToken)
        {
            return await _context.Image!.ToListAsync(cancellationToken);
        }
    }
}