using Dissertation.Infrastructure.Mediatr.Csirp.Queries.GetPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dissertation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CsirpController : ApiControllerBase
{
    [HttpGet]
    public async Task<PlanDto> GetPlan([FromQuery] GetPlanQuery query)
        => await Mediator.Send(query);
}
