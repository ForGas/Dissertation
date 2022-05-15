using Dissertation.Common.Services;
using Dissertation.Common.Services.DirectoryService;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries
{
    public class GetSha256Query : IRequest<string>
    {
        public IFormFile File { get; set; } = default!;
    }

    public class GetSha256QueryHandler
       : IRequestHandler<GetSha256Query, string>
    {
        private readonly IFileService _fileService;
        private readonly IScanInfoService _scanInfoService;
        private readonly IApplicationDbContext _context;

        public GetSha256QueryHandler(IFileService fileService, IApplicationDbContext context,
        IScanInfoService scanInfoService) =>
            (_fileService, _context, _scanInfoService) = (fileService, context, scanInfoService);

        public async Task<string> Handle(GetSha256Query request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request.File);

            var stream = request.File.OpenReadStream();

            using SHA256 sha = SHA256.Create();
            var buffer = await sha.ComputeHashAsync(stream);

            var stringBuilder = new StringBuilder(buffer.Length * 2);
            foreach (byte b in buffer)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            var result = stringBuilder.ToString();

            return result;
        }
    }
}
