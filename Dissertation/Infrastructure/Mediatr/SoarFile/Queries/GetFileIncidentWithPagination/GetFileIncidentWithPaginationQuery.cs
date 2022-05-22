using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dissertation.Common.Services;
using Dissertation.Infrastructure.Common;
using Dissertation.Infrastructure.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetFileIncidentWithPagination;

public class GetFileIncidentWithPaginationQuery : PaginatedQuery, IRequest<PaginatedList<FileIncidentDto>> { }

public class GetFileIncidentWithPaginationQueryHandler : IRequestHandler<GetFileIncidentWithPaginationQuery, PaginatedList<FileIncidentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileIncidentWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper
        ) => (_context, _mapper) = (context, mapper);
    public async Task<PaginatedList<FileIncidentDto>> Handle(GetFileIncidentWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.FileIncidents
                    .Include(x => x.Details).ThenInclude(x => x!.Report)
                    .OrderByDescending(x => x.Created)
                    .ProjectTo<FileIncidentDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
