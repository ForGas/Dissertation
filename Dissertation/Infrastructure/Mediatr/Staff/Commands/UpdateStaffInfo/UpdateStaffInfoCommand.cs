using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Staff.Commands.UpdateStaffInfo;

public class UpdateStaffInfoCommand : IRequest<Unit>
{

}

public class UpdateStaffInfoCommandHandler : IRequestHandler<UpdateStaffInfoCommand, Unit>
{
    public Task<Unit> Handle(UpdateStaffInfoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
