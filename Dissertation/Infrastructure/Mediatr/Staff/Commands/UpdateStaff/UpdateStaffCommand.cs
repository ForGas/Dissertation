using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Staff.Commands.UpdateStaff;

public class UpdateStaffCommand : IRequest<Unit>
{

}

public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, Unit>
{
    public Task<Unit> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}