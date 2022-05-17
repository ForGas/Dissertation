using MediatR;
using Dissertation.Infrastructure.Common;

namespace Dissertation.Infrastructure.Mediatr.Staff.Queries.GetStaffsWithPagination;

public record class GetStaffsWithPaginationQuery() : IRequest<PaginatedList<StaffDto>>;


public class GetStaffsWithPaginationQueryHandler : IRequestHandler<GetStaffsWithPaginationQuery, PaginatedList<StaffDto>>
{
    public Task<PaginatedList<StaffDto>> Handle(GetStaffsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}