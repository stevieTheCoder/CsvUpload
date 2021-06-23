using CsvUpload.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvUpload.Application.MeterReadings.Commands.CreatingReadingsFromBulkFile
{
    public class CreateReadingsFromBulkFileCommand : IRequest<int>
    {
        public IReadOnlyCollection<MeterReadingDto> MeterReadings { get; }

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

        public async Task<int> Handle(CreateReadingsFromBulkFileCommand request, CancellationToken cancellationToken)
        {
            // Retrieve accounts which match ids in file 
            var accounts = await _context.Accounts
                .Where(a => request.MeterReadings
                .Select(mr => mr.Id).Contains(a.Id))
                .Include(a => a.MeterReadings)
                .ToListAsync(cancellationToken);

            // Add meter readings to account
            foreach (var account in accounts)
            {
                var readings = request.MeterReadings.Where(mr => mr.Id == account.Id);
                
                foreach (var reading in readings)
                {
                    account.AddMeterReading(reading.Date, reading.Value);                 
                }
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
