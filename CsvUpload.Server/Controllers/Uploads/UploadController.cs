using CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile;
using CsvUpload.Server.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CsvUpload.Server.Controllers.Uploads
{

    [Route("meter-reading-uploads")]
    public class UploadController : ApiController
    {
        private readonly ILogger<UploadController> _logger;
        private readonly ICsvHelper _csvHelper;

        public UploadController(ILogger<UploadController> logger, ICsvHelper csvHelper)
        {
            _logger = logger;
            _csvHelper = csvHelper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Upload([FromForm] FileDataRequestModel fileData)
        {
            try
            {
                var records = _csvHelper.GetRecords<MeterReadingDto>(fileData.file);

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
