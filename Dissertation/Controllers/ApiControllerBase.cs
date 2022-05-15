using MediatR;
using Microsoft.AspNetCore.Mvc;

#nullable disable
namespace Dissertation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}
