using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CsvUpload.Server.Helpers
{
    public interface ICsvHelper
    {
        IReadOnlyCollection<T> GetRecords<T>(IFormFile file);
    }
}
