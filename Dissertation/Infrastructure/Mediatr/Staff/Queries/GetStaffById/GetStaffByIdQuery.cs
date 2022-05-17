using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Staff.Queries.GetStaffById;

public record class GetStaffByIdQuery(Guid Id) : IRequest<StaffDto>;


public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDto>
{
    public Task<StaffDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
