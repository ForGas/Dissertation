using MediatR;
using RestSharp;
using Newtonsoft.Json;
using VirusTotalNet.Results;
using Dissertation.Common.Services;

namespace Dissertation.Infrastructure.Mediatr.SoarFile.Queries;

public record class GetVirusTotalReportQuery(string ResourceId) : IRequest<string>;

public class GetVirusTotalReportQueryHandler : IRequestHandler<GetVirusTotalReportQuery, string>
{
    private readonly IScanInfoService _scanInfoService;
    private readonly IApplicationDbContext _context;

    public GetVirusTotalReportQueryHandler(IApplicationDbContext context,
    IScanInfoService scanInfoService) =>
        (_context, _scanInfoService) = (context, scanInfoService);

    public async Task<string> Handle(GetVirusTotalReportQuery request, CancellationToken cancellationToken)
    {
        var client = new RestClient(_scanInfoService.VirusTotalReportUrl);
        var virusTotalrequest = new RestRequest();

        virusTotalrequest.AddQueryParameter("apikey", _scanInfoService.VirusTotalApiKey);
        virusTotalrequest.AddQueryParameter("resource", request.ResourceId);
        virusTotalrequest.AddQueryParameter("allinfo", "false");

        var response = await client.ExecuteGetAsync(virusTotalrequest);

        var result = JsonConvert.SerializeObject(response.Content);

        return response.Content;
    }
}
