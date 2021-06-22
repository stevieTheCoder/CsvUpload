using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CsvUpload.Server.Helpers
{
    public interface ICsvHelper
    {
        IEnumerable<T> GetRecords<T>(IFormFile file);
    }
}
