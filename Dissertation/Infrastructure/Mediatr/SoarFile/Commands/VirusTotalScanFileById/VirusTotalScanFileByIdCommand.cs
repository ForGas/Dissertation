using MediatR;
using AutoMapper;
using Newtonsoft.Json;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Dissertation.Infrastructure.Mediatr.SoarFile.Common;
using Dissertation.Common.Services.DirectoryService;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFileById;

public record class VirusTotalScanFileByIdCommand(Guid FileIncidentId) : IRequest<Unit>;

public class VirusTotalScanFileByIdCommandHandler 
    : BaseVirusScanFileHandler, IRequestHandler<VirusTotalScanFileByIdCommand, Unit>
{
    private readonly IMapper _mapper;

    public VirusTotalScanFileByIdCommandHandler(
            IFileService fileService,
            IMapper mapper,
            IApplicationDbContext context,
            IScanInfoService scanInfoService,
            IDateTime dateTime
        ) : base(fileService, context, scanInfoService, dateTime) => (_mapper) = (mapper);

    public async Task<Unit> Handle(VirusTotalScanFileByIdCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.FileIncidentId);

        var incident = await _context.FileIncidents.FindAsync(request.FileIncidentId);
        var content = await GetReportDetailsByFilePathAsync(incident!.FullPath);

        var scanResult = JsonConvert.DeserializeObject<VirusTotalScanResultDto>(content) ?? new();

        var fileDetails = _mapper.Map<FileDetails>(scanResult);
        var report = _mapper.Map<VirusTotalReportDetails>(scanResult);

        incident.IsVirusHashInfoClean = !(_context.VirusHashInfo
            .Any(x => x.Sha256 == fileDetails.Sha256 && x.IsVirus));
        fileDetails.Incident = incident;
        report.FileDetails = fileDetails;

        _context.FileIncidents.Update(incident);
        _context.FileDetails.Add(fileDetails);
        _context.VirusTotalReportDetails.Add(report);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}