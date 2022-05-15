using MediatR;
using AutoMapper;
using System.Diagnostics;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.SystemVirusScanFile;

public record class SystemVirusScanFileCommand(IFormFile File) : IRequest<SystemVirusScanFileDto>;

public class VirusScanFileCommandHandler : IRequestHandler<SystemVirusScanFileCommand, SystemVirusScanFileDto>
{
    private readonly IFileService _fileService;
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public VirusScanFileCommandHandler(IFileService fileService, IApplicationDbContext context,
        IScanInfoService scanInfoService, IMapper mapper) =>
            (_fileService, _context, _scanInfoService, _mapper) = (fileService, context, scanInfoService, mapper);
    
    public async Task<SystemVirusScanFileDto> Handle(SystemVirusScanFileCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.File);

        var incident = new FileIncident();

        var fileExtention = Path.GetExtension(request.File.FileName);
        var fileName = $"{incident.Id}{fileExtention}";
        var fullPath = Path.Combine(
            _fileService.GetDirectoryPath(),
            _scanInfoService.FileStorageFolderName, fileName);

        using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            await request.File.CopyToAsync(fileStream, CancellationToken.None);
        }

        incident.FullPath = fullPath;
        incident.FileName = fileName;
        incident.FolderName = _scanInfoService.FileStorageFolderName;

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
        var result = process.ExitCode == 0 
                ? SystemScanStatus.Clear 
                : SystemScanStatus.Analysis;

        incident.Status = result;

        _context.FileIncidents.Add(incident);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SystemVirusScanFileDto>(incident);
    }
}