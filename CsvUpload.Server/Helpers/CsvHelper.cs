using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvUpload.Server.Helpers
{
    public class CsvHelper : ICsvHelper
    {
        public IEnumerable<T> GetRecords<T>(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }
    }
}
