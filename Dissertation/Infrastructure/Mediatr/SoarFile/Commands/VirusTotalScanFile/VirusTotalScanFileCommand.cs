using MediatR;
using AutoMapper;
using Newtonsoft.Json;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;
using Dissertation.Common.Services.DirectoryService;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFile;

public record class VirusTotalScanFileCommand(IFormFile File) : IRequest<VirusTotalScanResultDto>;

public class VirusTotalScanFileCommandHandler
    : IRequestHandler<VirusTotalScanFileCommand, VirusTotalScanResultDto>
{
    private readonly IFileService _fileService;
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public VirusTotalScanFileCommandHandler(IFileService fileService, IMapper mapper,
        IApplicationDbContext context, IScanInfoService scanInfoService) =>
        (_fileService, _context, _scanInfoService, _mapper) = (fileService, context, scanInfoService, mapper);

    public async Task<VirusTotalScanResultDto> Handle(VirusTotalScanFileCommand request, CancellationToken cancellationToken)
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
        incident.Status = SystemScanStatus.NoDefinition;

        var data = new[]
        {
            new KeyValuePair<string, string>("apikey", _scanInfoService.VirusTotalApiKey),
            new KeyValuePair<string, string>("file", incident.FullPath),
        };

        using var client = new HttpClient();
        using var result = await client.PostAsync(_scanInfoService.VirusTotalScanUrl, new FormUrlEncodedContent(data));
        var content = await result.Content.ReadAsStringAsync(CancellationToken.None);

        var scanResult = JsonConvert.DeserializeObject<VirusTotalScanResultDto>(content);

        var fileDetails = _mapper.Map<FileDetails>(scanResult);
        var report = _mapper.Map<VirusTotalReportDetails>(scanResult);
        fileDetails.Incident = incident;
        report.FileDetails = fileDetails;

        _context.FileIncidents.Add(incident);
        _context.FileDetails.Add(fileDetails);
        _context.VirusTotalReportDetails.Add(report);

        await _context.SaveChangesAsync(cancellationToken);

        return scanResult;
    }
}
