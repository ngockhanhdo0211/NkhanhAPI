using Microsoft.AspNetCore.Http;
namespace NkhanhAPI.Models.DTO
{
    public class ImageUploadRequestDto
    {
        public IFormFile File { get; set; }
        public string? Description { get; set; }
    }
}
