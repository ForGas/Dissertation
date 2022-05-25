using Dissertation.Infrastructure.Mediatr.Csirp.Queries.GetPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dissertation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CsirpController : ApiControllerBase
{
    [HttpGet]
    public async Task<Unit> GetPlan()
        => await Mediator.Send(new GetPlanQuery());
}
