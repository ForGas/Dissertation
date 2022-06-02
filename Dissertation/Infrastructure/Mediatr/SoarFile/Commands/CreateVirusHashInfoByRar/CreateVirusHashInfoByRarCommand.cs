using Dissertation.Common.Services;
using Dissertation.Common.Services.DirectoryService;
using Dissertation.Persistence.Entities;
using MediatR;
using SharpCompress.Archives.Rar;
using System.Security.Cryptography;
using System.Text;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.CreateVirusHashInfoByRar;

public record class CreateVirusHashInfoByRarCommand(IFormFile RarFile) : IRequest<Unit>;


public class CreateVirusHashInfoByRarCommandHandler : IRequestHandler<CreateVirusHashInfoByRarCommand, Unit>
{
    private readonly IFileService _fileService;
    private readonly IApplicationDbContext _context;
    private readonly IScanInfoService _scanInfoService;

    public CreateVirusHashInfoByRarCommandHandler(
            IFileService fileService,
            IApplicationDbContext context,
            IScanInfoService scanInfoService
        ) => (_fileService, _context, _scanInfoService) = (fileService, context, scanInfoService);

    public async Task<Unit> Handle(CreateVirusHashInfoByRarCommand request, CancellationToken cancellationToken)
    {
        var fileExtention = Path.GetExtension(request.RarFile.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtention}";
        var fullPath = Path.Combine(
            _fileService.GetDirectoryPath(),
            _scanInfoService.FileStorageFolderName, fileName);

        using (var fileStream = new FileStream(fullPath, FileMode.Create))
        {
            await request.RarFile.CopyToAsync(fileStream, CancellationToken.None);
        }

        using var archive = RarArchive.Open(fullPath);
        var list = new List<VirusHashInfo>() { Capacity = archive.Entries.Count };
        using SHA256 sha = SHA256.Create();

        foreach (var item in archive.Entries)
        {
            var stream = item.OpenEntryStream();
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
                Title = item.Key.Split('\\').Last(),
            });
        }

        await _context.VirusHashInfo.AddRangeAsync(list);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}