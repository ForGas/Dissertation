using Dissertation.Common.Services;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Common;

public abstract class BaseVirusScanFileHandler
{
    protected readonly IApplicationDbContext _context;
    protected readonly IFileService _fileService;
    protected readonly IScanInfoService _scanInfoService;
    protected readonly IDateTime _dateTime;

    public BaseVirusScanFileHandler(
        IFileService fileService,
        IApplicationDbContext context, 
        IScanInfoService scanInfoService,
        IDateTime dateTime) =>
        (_fileService, _context, _scanInfoService, _dateTime) = (fileService, context, scanInfoService, dateTime);

    protected async Task<FileIncident> GetFileIncidentByFileAsync(IFormFile file)
    {
        var incident = new FileIncident();
        var count = await _context.FileIncidents.CountAsync();
        var date = _dateTime.Now;

        var fileExtention = Path.GetExtension(file.FileName);
        var fileName = $"{incident.Id}{fileExtention}";
        var fullPath = Path.Combine(
            _fileService.GetDirectoryPath(),
            _scanInfoService.FileStorageFolderName, fileName);

        using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            await file.CopyToAsync(fileStream, CancellationToken.None);
        }

        incident.Code = $"F-{date.Day}/{date.Month}/{date.Year}-{count}";
        incident.FullPath = fullPath;
        incident.FileName = fileName;
        incident.FolderName = _scanInfoService.FileStorageFolderName;

        return incident;
    }

    protected async Task<string> GetReportDetailsByFilePathAsync(string fileFullPath)
    {
        var data = new[]
        {
            new KeyValuePair<string, string>("apikey", _scanInfoService.VirusTotalApiKey),
            new KeyValuePair<string, string>("file", fileFullPath),
        };

        using var client = new HttpClient();
        using var result = await client.PostAsync(_scanInfoService.VirusTotalScanUrl, new FormUrlEncodedContent(data));
        var content = await result.Content.ReadAsStringAsync(CancellationToken.None);

        return content;
    }
}
