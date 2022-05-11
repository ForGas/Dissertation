using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dissertation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<ISender>() ?? throw new Exception("Mediatr Exception");
    }
}
