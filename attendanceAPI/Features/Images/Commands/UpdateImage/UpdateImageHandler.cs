using Ardalis.Result;
using attendanceAPI.Data;
using attendanceAPI.Models;
using MediatR;
using DrawingImage = SixLabors.ImageSharp;



namespace attendanceAPI.Features.Images.Commands.UpdateImage
{
    public class UpdateImageHandler : IRequestHandler<UpdateImageCommand, Result<Image>>
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UpdateImageHandler(AppDbContext appDbContext, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _env = env;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<Result<Image>> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                return Result.Invalid();


            var image = await _context.Image!.FindAsync(new object?[] { request.Id }, cancellationToken);

            if (image == null)
                return Result.NotFound();

            var oldFile = Path.Combine(_env.WebRootPath, "images/" + image.Name);

            if (File.Exists(oldFile))
                File.Delete(oldFile);


            var imageFolder = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            string extension = Path.GetExtension(request.File.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            var requestContext = _httpContextAccessor.HttpContext!;
            var fileUrl = $"{requestContext.Request.Scheme}://{requestContext.Request.Host}/images/{fileName}";

            int width;
            int height;

            using (var img = DrawingImage.Image.Load(filePath))
            {
                width = img.Width;
                height = img.Height;
            }

            image.Name = fileName;
            image.Path = fileUrl;
            image.Width = width.ToString();
            image.Height = height.ToString();
            image.Size = request.File.Length.ToString();
            image.Type = request.File.ContentType;



            _context.Image!.Update(image);
            await _context.SaveChangesAsync(cancellationToken);


            return Result.Success(image);

        }
    }
}