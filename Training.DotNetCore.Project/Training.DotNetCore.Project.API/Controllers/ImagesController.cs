//using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.Project.API.DTO;
using Training.DotNetCore.Project.API.Models.Domain;
using Training.DotNetCore.Project.API.Repositories;

namespace Training.DotNetCore.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        //private readonly IMapper mapper;

        public ImagesController(IImageRepository imageRepository//, IMapper mapper
            )
        {
            this.imageRepository = imageRepository;
            //this.mapper = mapper;
        }

        //POST api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //Convert DTOs to Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };
                //request.FileExtension = Path.GetExtension(request.File.FileName).ToLower();
                //var imageDomainModel = mapper.Map<Image>(request);

                //Call User Repository to upload file
                await imageRepository.Upload(imageDomainModel);

                //Convert Domain Model to DTO
                //var imageDto = mapper.Map<ImageUploadRequestDto>(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            //Validate the extension and file size
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (allowedExtension.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (request.File.Length > 10485760) //10 MB
            {
                ModelState.AddModelError("File", "File Size More than 10MB, Please upload a smaller size file");
            }
        }
    }
}
