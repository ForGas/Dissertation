using MediatR;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dissertation.Common.Services;
using Microsoft.EntityFrameworkCore;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

#nullable disable
namespace Dissertation.Infrastructure.Mediatr.SoarFile.Commands.FillUpVirusTotalReportById;

public record class FillUpVirusTotalReportByIdCommand(Guid VirusTotalReportDetailId) : IRequest<ReportStatus>;

public class FillUpVirusTotalReportByIdCommandHandler : IRequestHandler<FillUpVirusTotalReportByIdCommand, ReportStatus>
{
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;

    public FillUpVirusTotalReportByIdCommandHandler(IApplicationDbContext context,
    IScanInfoService scanInfoService) =>
        (_context, _scanInfoService) = (context, scanInfoService);

    public async Task<ReportStatus> Handle(FillUpVirusTotalReportByIdCommand request, CancellationToken cancellationToken)
    {
        var report = _context.VirusTotalReportDetails
            .Include(x => x.FileDetails).ThenInclude(x => x.Incident)
            .FirstOrDefault(x => x.Id == request.VirusTotalReportDetailId);

        ArgumentNullException.ThrowIfNull(report);
        if (!string.IsNullOrEmpty(report.JsonContent))
        {
            return ReportStatus.IsReady;
        }

        var virusTotalrequest = new RestRequest();
        virusTotalrequest.AddQueryParameter("apikey", _scanInfoService.VirusTotalApiKey);
        virusTotalrequest.AddQueryParameter("resource", report.Resource);
        virusTotalrequest.AddQueryParameter("allinfo", "false");

        using var client = new RestClient(_scanInfoService.VirusTotalReportUrl);
        var response = await client.ExecuteGetAsync(virusTotalrequest);

        var content = response.Content;
        var reportResult = JsonConvert.DeserializeObject<ReportResult>(content);

        if (reportResult == null || reportResult.Code == -2)
        {
            return ReportStatus.InQueue;
        }

        dynamic jObject = JObject.Parse(content);
        var list = new List<VirusScanReportDto>() { Capacity = 58 };

        foreach (var item in jObject.scans)
        {
            VirusScanReportDto virus = JsonConvert.DeserializeObject<VirusScanReportDto>(Convert.ToString(item.Value));
            virus.Name = item.Name;
            list.Add(virus);
        }

        var incident = report.FileDetails.Incident;
        incident.Status = list.Any(x => x.Detected) ? ScanStatus.Virus : ScanStatus.Clean;
        incident.Priority = incident.Status == ScanStatus.Virus ? Priority.High : Priority.Low;

        var result = JsonConvert.SerializeObject(list, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        report.JsonContent = result;
        _context.VirusTotalReportDetails.Update(report);
        _context.FileIncidents.Update(incident);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return ReportStatus.IsReady;
    }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum ReportStatus
{
    InQueue = 0,
    IsReady = 1,
}

public class ReportResult
{
    [JsonProperty("response_code")]
    public int Code { get; set; }
}