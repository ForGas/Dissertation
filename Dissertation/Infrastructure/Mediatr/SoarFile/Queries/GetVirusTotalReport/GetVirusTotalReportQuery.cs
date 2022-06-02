using MediatR;
using AutoMapper;
using Dissertation.Common.Services;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.FillUpVirusTotalReportById;

#nullable disable
namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetVirusTotalReport;

public record class GetVirusTotalReportQuery(Guid ReportId) : IRequest<string>;

public class GetVirusTotalReportQueryHandler : IRequestHandler<GetVirusTotalReportQuery, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _mediatr;

    public GetVirusTotalReportQueryHandler(
            IApplicationDbContext context, 
            IMapper mapper,
            ISender mediatr
        ) => (_context, _mapper, _mediatr) = (context, mapper, mediatr);

    public async Task<string> Handle(GetVirusTotalReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _context.VirusTotalReportDetails.FindAsync(request.ReportId);

        if (string.IsNullOrEmpty(report.JsonContent))
        {
            _ = await _mediatr.Send(new FillUpVirusTotalReportByIdCommand(report.Id));

            return _context.VirusTotalReportDetails
                .FirstOrDefault(x => x.Id == report.Id)
                .JsonContent;
        }

        return report.JsonContent;
    }
}
