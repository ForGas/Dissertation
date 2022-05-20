using MediatR;
using Dissertation.Common.Services;

#nullable disable
namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries;

public record class GetVirusTotalReportQuery(Guid ReportId) : IRequest<string>;

public class GetVirusTotalReportQueryHandler : IRequestHandler<GetVirusTotalReportQuery, string>
{
    private readonly IApplicationDbContext _context;

    public GetVirusTotalReportQueryHandler(IApplicationDbContext context) =>
        (_context) = (context);

    public async Task<string> Handle(GetVirusTotalReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.VirusTotalReportDetails.FindAsync(request.ReportId, cancellationToken);
        return report.JsonContent;
    }
}
