using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.DotNetCore.Project.API.DTO
{
    public class ImageUploadRequestDto
    {


        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        //public string FileExtension { get; set; }
        //    public long FileSizeInBytes { get; set; }
        //    public string FilePath { get; set; }
    }
}
