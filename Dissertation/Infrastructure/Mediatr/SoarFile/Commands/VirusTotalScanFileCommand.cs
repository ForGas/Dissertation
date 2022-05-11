using MediatR;
using Leaf.xNet;
using Newtonsoft.Json;
using VirusTotalNet.Results;
using Dissertation.Common.Services;
using Dissertation.Persistence.Entities;
using HttpRequest = Leaf.xNet.HttpRequest;
using Dissertation.Common.Services.DirectoryService;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands
{
    public class VirusTotalScanFileCommand : IRequest<ScanResult>
    {
        public IFormFile File { get; set; } = default!;
        public string IpAddrees { get; set; } = default!;
        public string Domain { get; set; } = default!;
    }

    public class VirusTotalScanFileCommandHandler
        : IRequestHandler<VirusTotalScanFileCommand, ScanResult>
    {
        private readonly IFileService _fileService;
        private readonly IScanInfoService _scanInfoService;
        private readonly IApplicationDbContext _context;

        public VirusTotalScanFileCommandHandler(IFileService fileService, IApplicationDbContext context,
        IScanInfoService scanInfoService) =>
            (_fileService, _context, _scanInfoService) = (fileService, context, scanInfoService);

        public async Task<ScanResult> Handle(VirusTotalScanFileCommand request, CancellationToken cancellationToken)
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

            incident.Domain = request.Domain;
            incident.IpAddrees = request.IpAddrees;
            incident.FullPath = fullPath;
            incident.FileName = fileName;
            incident.FolderName = _scanInfoService.FileStorageFolderName;

            using var virusTotalrequest = new HttpRequest();

            var parameters = new RequestParams();
            parameters["apikey"] = _scanInfoService.VirusTotalApiKey;
            parameters["file"] = fullPath;
            virusTotalrequest.Dispose();

            var result = virusTotalrequest
                    .Post(_scanInfoService.VirusTotalScanUrl, parameters);

            
            var scanResult = JsonConvert.DeserializeObject<ScanResult>(result.ToString());

            return scanResult;
        }
    }
}