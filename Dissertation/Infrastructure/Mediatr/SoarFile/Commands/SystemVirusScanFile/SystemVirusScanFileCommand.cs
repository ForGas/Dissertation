using MediatR;
using AutoMapper;
using System.Diagnostics;
using Dissertation.Common.Services;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Persistence.Entities.Common;
using Dissertation.Infrastructure.Mediatr.SoarFile.Common;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFileById;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.SystemVirusScanFile;

public record class SystemVirusScanFileCommand(IFormFile File) : IRequest<SystemVirusScanFileDto>;

public class VirusScanFileCommandHandler 
    : BaseVirusScanFileHandler, IRequestHandler<SystemVirusScanFileCommand, SystemVirusScanFileDto>
{
    private readonly IMapper _mapper;
    private readonly ISender _mediatr;

    public VirusScanFileCommandHandler(
            IFileService fileService,
            IMapper mapper,
            IApplicationDbContext context,
            IScanInfoService scanInfoService,
            ISender mediatr
        ) : base(fileService, context, scanInfoService) => (_mapper, _mediatr) = (mapper, mediatr);

    public async Task<SystemVirusScanFileDto> Handle(SystemVirusScanFileCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.File);

        var incident = await GetFileIncidentByFileAsync(request.File);

        using var process = new Process();
        var processStartInfo = new ProcessStartInfo(_scanInfoService.AntivirusScanInSystemCommand)
        {
            Arguments = $"-Scan -ScanType 3 -File \"{incident.FullPath}\" -Timeout 1 -DisableRemediation",
            CreateNoWindow = true,
            ErrorDialog = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = true
        };

        process.StartInfo = processStartInfo;
        process.Start();
        await process.WaitForExitAsync(cancellationToken);

        incident.Status = process.ExitCode == 0 
            ? SystemScanStatus.Clear 
            : SystemScanStatus.Analysis;

        _context.FileIncidents.Add(incident);

        var result = await _context.SaveChangesAsync(cancellationToken);
        await _mediatr.Send(new VirusTotalScanFileByIdCommand(incident.Id));

        return _mapper.Map<SystemVirusScanFileDto>(incident);
    }
}