using MediatR;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.CreateVirusTotalReport;

public record class CreateVirusTotalReportCommand(string ResourceId) : IRequest<string>;

public class CreateVirusTotalReportCommandHandler : IRequestHandler<CreateVirusTotalReportCommand, string>
{
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;

    public CreateVirusTotalReportCommandHandler(IApplicationDbContext context,
    IScanInfoService scanInfoService) =>
        (_context, _scanInfoService) = (context, scanInfoService);

    public async Task<string> Handle(CreateVirusTotalReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _context.VirusTotalReportDetails
            .FirstOrDefaultAsync(x=> x.Resource == request.ResourceId);

        ArgumentNullException.ThrowIfNull(report);

        using var client = new RestClient(_scanInfoService.VirusTotalReportUrl);
        var virusTotalrequest = new RestRequest();

        virusTotalrequest.AddQueryParameter("apikey", _scanInfoService.VirusTotalApiKey);
        virusTotalrequest.AddQueryParameter("resource", request.ResourceId);
        virusTotalrequest.AddQueryParameter("allinfo", "false");

        var response = await client.ExecuteGetAsync(virusTotalrequest);

        dynamic jObject = JObject.Parse(response.Content ?? string.Empty);
        var list = new List<VirusScanReportDto>() { Capacity = 58 };

        foreach (var item in jObject.scans)
        {
            VirusScanReportDto virus = JsonConvert.DeserializeObject<VirusScanReportDto>(Convert.ToString(item.Value));
            virus.Name = item.Name;
            list.Add(virus);
        }

        var result = JsonConvert.SerializeObject(list);
        report.JsonContent = result;
        _context.VirusTotalReportDetails.Update(report);
        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
