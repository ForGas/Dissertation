using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Staff.Commands.DeleteStaff;

public record class DeleteStaffCommand(Guid Id) : IRequest<Unit>;

public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, Unit>
{
    public Task<Unit> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
