using CsvUpload.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile
{
    public class CreateReadingsFromBulkFileCommand : IRequest<int>
    {
        IReadOnlyCollection<MeterReadingDto> MeterReadings { get; }

        public CreateReadingsFromBulkFileCommand(IReadOnlyCollection<MeterReadingDto> meterReadings)
        {
            MeterReadings = meterReadings;
        }
    }

    internal class CreateReadingsFromBulkFileCommandHandler : IRequestHandler<CreateReadingsFromBulkFileCommand, int>
    {
        private readonly IApplicationContext _context;

        public CreateReadingsFromBulkFileCommandHandler(IApplicationContext context)
        {
            _context = context;
        }

        public Task<int> Handle(CreateReadingsFromBulkFileCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}
