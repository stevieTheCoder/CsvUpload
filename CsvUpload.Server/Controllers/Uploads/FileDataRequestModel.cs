using CsvUpload.Server.Helpers;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CsvUpload.Server.Controllers.Uploads
{
    public class FileDataRequestModel
    {
        [AllowedExtensions(new string[] { ".csv" })]
        [Required(ErrorMessage = "A file is required")]
        public IFormFile file { get; set; }
    }
}
