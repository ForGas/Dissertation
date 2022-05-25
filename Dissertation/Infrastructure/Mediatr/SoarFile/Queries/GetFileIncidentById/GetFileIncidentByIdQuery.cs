using MediatR;
using AutoMapper;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries.GetFileIncidentById;

public record class GetFileIncidentByIdQuery(Guid IncidentId) : IRequest<FileIncidentDto>;

public class GetFileIncidentByIdQueryHandler : IRequestHandler<GetFileIncidentByIdQuery, FileIncidentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFileIncidentByIdQueryHandler(IApplicationDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<FileIncidentDto> Handle(GetFileIncidentByIdQuery request, CancellationToken cancellationToken)
    {
        var incident = await _context.FileIncidents
            .Include(x => x.Details).ThenInclude(x => x!.Report)
            .FirstOrDefaultAsync(x => x.Id == request.IncidentId);
        
        return _mapper.Map<FileIncidentDto>(incident);
    }
}




