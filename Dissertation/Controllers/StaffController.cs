using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dissertation.Infrastructure.Common;
using Dissertation.Infrastructure.Mediatr.Staff.Queries;
using Dissertation.Infrastructure.Mediatr.Staff.Queries.GetStaffById;
using Dissertation.Infrastructure.Mediatr.Staff.Commands.CreateStaff;
using Dissertation.Infrastructure.Mediatr.Staff.Commands.UpdateStaffInfo;
using Dissertation.Infrastructure.Mediatr.Staff.Commands.UpdateStaff;
using Dissertation.Infrastructure.Mediatr.Staff.Commands.DeleteStaff;
using Dissertation.Infrastructure.Mediatr.Staff.Queries.GetStaffsWithPagination;

namespace Dissertation.Controllers;

[Route("api/[controller]/[action]")]
public class StaffController : ApiControllerBase
{
    [HttpPost]
    public async Task<Guid> Create([FromForm] CreateStaffCommand command)
        => await Mediator.Send(command);

    [HttpGet]
    [Route("{id}")]
    public async Task<StaffDto> GetById([FromQuery] Guid id)
        => await Mediator.Send(new GetStaffByIdQuery(id));

    [HttpGet]
    public async Task<PaginatedList<StaffDto>> GetAll([FromQuery] GetStaffsWithPaginationQuery query)
        => await Mediator.Send(query);

    [HttpPatch]
    public async Task<Unit> UpdateInfo([FromBody] UpdateStaffInfoCommand command)
        => await Mediator.Send(command);

    [HttpPut]
    public async Task<Unit> Update([FromBody] UpdateStaffCommand command)
        => await Mediator.Send(command);

    [HttpDelete]
    [Route("{id}")]
    public async Task<Unit> Delete([FromRoute] Guid id)
        => await Mediator.Send(new DeleteStaffCommand(id));
}