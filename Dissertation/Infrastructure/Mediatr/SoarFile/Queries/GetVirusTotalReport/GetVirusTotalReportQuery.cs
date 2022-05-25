using MediatR;
using Dissertation.Common.Services;
using AutoMapper;

#nullable disable
namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetVirusTotalReport;

public record class GetVirusTotalReportQuery(Guid ReportId) : IRequest<string>;

public class GetVirusTotalReportQueryHandler : IRequestHandler<GetVirusTotalReportQuery, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVirusTotalReportQueryHandler(IApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<string> Handle(GetVirusTotalReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.VirusTotalReportDetails.FindAsync(request.ReportId);
        return report.JsonContent;
    }
}
