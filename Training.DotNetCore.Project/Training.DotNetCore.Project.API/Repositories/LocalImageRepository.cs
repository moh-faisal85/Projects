using Training.DotNetCore.Project.API.Data;
using Training.DotNetCore.Project.API.Models.Domain;

namespace Training.DotNetCore.Project.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(
                webHostEnvironment.ContentRootPath,
                "Images", $"{image.FileName}{image.FileExtension}");

            //Upload Images to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //D:\Applications\Projects\Training.DotNetCore.Project\Training.DotNetCore.Project.API\Images
            //https://localhost:1234/images/image.jpg
            var req = httpContextAccessor.HttpContext.Request;
            var urlFilePath = $"{req.Scheme}://{req.Host}{req.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Images to the Images Database Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            //return image domain model
            return image;


        }
    }
}
