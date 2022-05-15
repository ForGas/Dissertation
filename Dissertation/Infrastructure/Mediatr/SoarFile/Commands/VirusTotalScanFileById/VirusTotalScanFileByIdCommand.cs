using MediatR;
using AutoMapper;
using Newtonsoft.Json;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFileById;

public record class VirusTotalScanFileByIdCommand(Guid FileIncidentId) : IRequest<Unit>;

public class VirusTotalScanFileByIdCommandHandler : IRequestHandler<VirusTotalScanFileByIdCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IScanInfoService _scanInfoService;

    public VirusTotalScanFileByIdCommandHandler(IApplicationDbContext context,
        IMapper mapper, IScanInfoService scanInfoService) =>
          (_context, _mapper, _scanInfoService) = (context, mapper, scanInfoService);

    public async Task<Unit> Handle(VirusTotalScanFileByIdCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.FileIncidentId);

        var incident = await _context.FileIncidents.SingleOrDefaultAsync(x => x.Id == request.FileIncidentId);

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

        _context.FileDetails.Add(fileDetails);
        _context.VirusTotalReportDetails.Add(report);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}