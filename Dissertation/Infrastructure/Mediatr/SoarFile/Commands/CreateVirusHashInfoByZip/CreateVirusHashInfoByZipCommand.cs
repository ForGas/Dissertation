using MediatR;
using Dissertation.Common.Services;
using Dissertation.Common.Services.DirectoryService;
using System.IO.Compression;
using Dissertation.Persistence.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.CreateVirusHashInfoByZip;

public record class CreateVirusHashInfoByZipCommand(IFormFile ZipFile) : IRequest<Unit>;

public class CreateVirusHashInfoByZipCommandHandler : IRequestHandler<CreateVirusHashInfoByZipCommand, Unit>
{
    private readonly IFileService _fileService;
    private readonly IApplicationDbContext _context;
    private readonly IScanInfoService _scanInfoService;

    public CreateVirusHashInfoByZipCommandHandler(
            IFileService fileService,
            IApplicationDbContext context,
            IScanInfoService scanInfoService
        ) => (_fileService, _context, _scanInfoService) = (fileService, context, scanInfoService);

    public async Task<Unit> Handle(CreateVirusHashInfoByZipCommand request, CancellationToken cancellationToken)
    {
        var fileExtention = Path.GetExtension(request.ZipFile.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtention}";
        var fullPath = Path.Combine(
            _fileService.GetDirectoryPath(),
            _scanInfoService.FileStorageFolderName, fileName);

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await request.ZipFile.CopyToAsync(fileStream, CancellationToken.None);
        }

        var archive = ZipFile.OpenRead(fullPath);
        var list = new List<VirusHashInfo>() { Capacity = archive.Entries.Count };
        using SHA256 sha = SHA256.Create();

        foreach (var item in archive.Entries)
        {
            var stream = item.Open();
            var buffer = await sha.ComputeHashAsync(stream);
            var stringBuilder = new StringBuilder(buffer.Length * 2);

            foreach (byte b in buffer)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            list.Add(new VirusHashInfo
            {
                IsVirus = true,
                Sha256 = stringBuilder.ToString(),
                Title = item.Name,
            });
        }

        await _context.VirusHashInfo.AddRangeAsync(list);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
