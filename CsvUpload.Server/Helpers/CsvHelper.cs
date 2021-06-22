using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvUpload.Server.Helpers
{
    public class CsvHelper : ICsvHelper
    {
        public IReadOnlyCollection<T> GetRecords<T>(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList().AsReadOnly();
        }
    }
}
