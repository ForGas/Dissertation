using MediatR;
using AutoMapper;
using Newtonsoft.Json;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Infrastructure.Mediatr.SoarFile.Common;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFile;

public record class VirusTotalScanFileCommand(IFormFile File) : IRequest<VirusTotalScanResultDto>;

public class VirusTotalScanFileCommandHandler
    : BaseVirusScanFileHandler, IRequestHandler<VirusTotalScanFileCommand, VirusTotalScanResultDto>
{
    private readonly IMapper _mapper;

    public VirusTotalScanFileCommandHandler(
            IFileService fileService, 
            IMapper mapper,
            IApplicationDbContext context, 
            IScanInfoService scanInfoService
        ) : base(fileService, context, scanInfoService) => (_mapper) = (mapper);

    public async Task<VirusTotalScanResultDto> Handle(VirusTotalScanFileCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.File);

        var incident = await GetFileIncidentByFileAsync(request.File);
        incident.Status = ScanStatus.Analysis;

        var content = await GetReportDetailsByFilePathAsync(incident.FullPath);
        var scanResult = JsonConvert.DeserializeObject<VirusTotalScanResultDto>(content) ?? new();

        var fileDetails = _mapper.Map<FileDetails>(scanResult);
        var report = _mapper.Map<VirusTotalReportDetails>(scanResult);
        fileDetails.Incident = incident;
        report.FileDetails = fileDetails;

        _context.FileIncidents.Add(incident);
        _context.FileDetails.Add(fileDetails);
        _context.VirusTotalReportDetails.Add(report);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return scanResult;
    }
}
