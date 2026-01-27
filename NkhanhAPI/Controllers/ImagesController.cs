using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;
using NkhanhAPI.Repositories;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Writer")]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly IImageRepository imageRepository;

        public ImagesController(
            IWebHostEnvironment environment,
            IImageRepository imageRepository)
        {
            this.environment = environment;
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(request.File.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format");
            }

            if (request.File.Length > 10_000_000)
            {
                return BadRequest("File size exceeds 10MB");
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(environment.ContentRootPath, "Images", fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream);

            var image = new NkhanhAPI.Models.Domain.Image
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                FileExtension = extension,
                FileSizeInBytes = request.File.Length,
                FilePath = filePath,
                CreatedAt = DateTime.UtcNow
            };


            await imageRepository.Upload(image);

            return Ok(image);
        }
    }
}
