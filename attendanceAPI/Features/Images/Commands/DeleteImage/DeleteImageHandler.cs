using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Features.Images.Commands.DeleteImage;
using attendanceAPI.Models;
using MediatR;


namespace attendanceAPI.Features.Images.Commands.CreateImage
{
    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, Result<Image>>
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteImageHandler(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _context = appDbContext;
            _env = env;
        }

        public async Task<Result<Image>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");

            var image = await _context.Image!.FindAsync(new object?[] { request.Id }, cancellationToken);

            if (image == null)
                return Result.NotFound();

            var filePath = Path.Combine(uploadsFolder, image.Name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.Image!.Remove(image);
            await _context.SaveChangesAsync(cancellationToken);


            return Result.Success(image);

        }
    }
}