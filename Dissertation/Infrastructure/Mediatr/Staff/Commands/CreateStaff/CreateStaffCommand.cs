using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Staff.Commands.CreateStaff;

public class CreateStaffCommand : IRequest<Guid>
{

}

public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, Guid>
{
    public Task<Guid> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
