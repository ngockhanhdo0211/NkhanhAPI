using NkhanhAPI.Data;
using NkhanhAPI.Models.Domain;

namespace NkhanhAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly NkhanhDbContext dbContext;

        public ImageRepository(NkhanhDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
