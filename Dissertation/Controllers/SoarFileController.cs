using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dissertation.Persistence.Entities;
using Dissertation.Infrastructure.Mediatr.SoarFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Queries;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.SystemVirusScanFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFile;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.VirusTotalScanFileById;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.FillUpVirusTotalReportById;
using Dissertation.Infrastructure.Mediatr.SoarFile.Commands.CreateVirusHashInfoByZip;

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
    [Route("{id}")]
    public async Task<string> GetVirusTotalReport([FromRoute] Guid id)
        => await Mediator.Send(new GetVirusTotalReportQuery(id));

    [HttpPatch]
    public async Task<Unit> FillUpVirusTotalReportById([FromQuery] FillUpVirusTotalReportByIdCommand command)
        => await Mediator.Send(command);

    [HttpGet]
    public async Task<string> GetSha256([FromForm] GetSha256Query query)
        => await Mediator.Send(query);

    [HttpPut]
    public async Task<Unit> VirusTotalScanById([FromQuery] VirusTotalScanFileByIdCommand command)
        => await Mediator.Send(command);

    [HttpPost]
    [RequestSizeLimit(bytes: 100_000_000)]
    public async Task<Unit> CreateVirusHashInfoByZip([FromForm] CreateVirusHashInfoByZipCommand command)
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