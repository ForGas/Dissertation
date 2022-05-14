using MediatR;
using System.Diagnostics;
using Dissertation.Persistence.Entities;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Persistence.Entities.Common;
using Dissertation.Common.Services;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands;

public class VirusScanFileCommand : IRequest<ScanStatus>
{
    public IFormFile File { get; set; } = default!;
}

public class VirusScanFileCommandHandler : IRequestHandler<VirusScanFileCommand, ScanStatus>
{
    private readonly IFileService _fileService;
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;

    public VirusScanFileCommandHandler(IFileService fileService, IApplicationDbContext context,
        IScanInfoService scanInfoService) =>
            (_fileService, _context, _scanInfoService) = (fileService, context, scanInfoService);
    
    public async Task<ScanStatus> Handle(VirusScanFileCommand request, CancellationToken cancellationToken)
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

        string filePath = incident.FullPath;

        using var process = new Process();
        var fileInfo = new FileInfo(filePath);
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
                ? ScanStatus.Clear 
                : ScanStatus.Analysis;

        return result;
    }
}