using Dissertation.Infrastructure.Mediatr.SoarFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.SystemVirusScanFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFileById;
using Dissertation.Infrastructure.Mediatr.SoarFile.Queries;
using Dissertation.Persistence.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dissertation.Controllers;

[Route("api/[controller]/[action]")]
public class SoarFileController : ApiControllerBase
{
    [HttpPost]
    [RequestSizeLimit(bytes: 100_000_000)]
    public async Task<SystemVirusScanFileDto> SystemVirusScan([FromForm] SystemVirusScanFileCommand command)
        => await Mediator.Send(command);

    [HttpPost]
    [RequestSizeLimit(bytes: 100_000_000)]
    public async Task<VirusTotalScanResultDto> VirusTotalScan([FromForm] VirusTotalScanFileCommand command)
        => await Mediator.Send(command);

    [HttpGet]
    public async Task<string> GetVirusTotalReport([FromQuery] GetVirusTotalReportQuery command)
        => await Mediator.Send(command);

    [HttpPost]
    public async Task<string> GetSha256([FromForm] GetSha256Query command)
        => await Mediator.Send(command);

    [HttpPost]
    public async Task<Unit> VirusTotalScanById([FromQuery] VirusTotalScanFileByIdCommand command)
        => await Mediator.Send(command);

    [HttpGet]
    public FileIncident Test()
    {
        var test = new FileIncident()
        {
            Id = Guid.NewGuid(),
            FullPath = "test",
            FolderName = "dsadas"
        };

        return test;
    }
}
