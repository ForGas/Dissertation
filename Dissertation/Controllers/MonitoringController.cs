using MediatR;
using Dissertation.Infrastructure.Mediatr.Monitoring.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Dissertation.Controllers;

[Route("api/[controller]/[action]")]
public class MonitoringController : ApiControllerBase
{

    [HttpGet]
    public Task<string> GetNetworkLog()
    {
        return Mediator.Send(new MonitorNetworkQuery());
    }

    [HttpGet]
    public Task<string> GetJournalLog()
    {
        return Mediator.Send(new MonitorJournalQuery());
    }

    [HttpGet]
    public Task<Unit> GetHardDriveLog()
    {
        return Mediator.Send(new MonitorHardDriveQuery());
    }
}
