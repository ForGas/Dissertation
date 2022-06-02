using AutoMapper;
using Dissertation.Common.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetReports;

public record class GetReportsQuery() : IRequest<List<ReportDto>>;


public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, List<ReportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetReportsQueryHandler(IApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<List<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var virusTotalReportDetails = await _context.VirusTotalReportDetails
            .Include(x => x.FileDetails)
            .Where(x => x.JsonContent != null)
            .ToListAsync();

        return _mapper.Map<List<ReportDto>>(virusTotalReportDetails);
    }
}
