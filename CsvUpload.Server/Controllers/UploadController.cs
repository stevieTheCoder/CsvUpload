using CsvHelper;
using CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CsvUpload.Server.Controllers
{

    [Route("meter-reading-uploads")]
    public class UploadController : ApiController
    {
        private readonly ILogger<UploadController> _logger;

        [HttpPost]
        public async Task<ActionResult<int>> Upload(IFormFile file)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<MeterReadingDto>().ToList().AsReadOnly();
                return await Mediator.Send(new CreateReadingsFromBulkFileCommand(records));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error parsing CSV", ex.Message);
                return BadRequest();
            }
        }
    }
}
