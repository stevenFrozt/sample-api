using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Models;
using MediatR;
using DrawingImage = SixLabors.ImageSharp;


namespace attendanceAPI.Features.Images.Commands.CreateImage
{
    public class CreateImageHandler : IRequestHandler<CreateImageCommand, Result<Image>>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateImageHandler(AppDbContext appDbContext, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<Image>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string extension = Path.GetExtension(request.File.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            var requestContext = _httpContextAccessor.HttpContext!;
            var fileUrl = $"{requestContext.Request.Scheme}://{requestContext.Request.Host}/images/{uniqueFileName}";

            int width;
            int height;

            using (var img = DrawingImage.Image.Load(filePath))
            {
                width = img.Width;
                height = img.Height;
            }

            var image = new Image
            {
                Name = uniqueFileName,
                Path = fileUrl,
                Size = request.File.Length.ToString(),
                Type = request.File.ContentType,
                Width = width.ToString(),
                Height = height.ToString()

            };

            await _appDbContext.Image!.AddAsync(image, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(image);

        }
    }
}