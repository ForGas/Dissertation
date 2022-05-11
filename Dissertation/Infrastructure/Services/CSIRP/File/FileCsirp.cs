using RestSharp;
using System.Diagnostics;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities;

namespace Dissertation.Infrastructure.Services.CSIRP.File;

public class FileCsirp
{
    public void DefineSecurityIncident(IIncident incident)
    {
        var fileIncident = (FileIncident)incident;

        ArgumentNullException.ThrowIfNull(fileIncident.FullPath);

        var filePath = @"D:\Test.txt";
        using var process = new Process();
        var fileInfo = new FileInfo(filePath);
        var processStartInfo = new ProcessStartInfo("C:/Program Files/Windows Defender/MpCmdRun.exe")
        {
            Arguments = $"-Scan -ScanType 3 -File \"{fileInfo.FullName}\" -DisableRemediation",
            CreateNoWindow = true,
            ErrorDialog = false,
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false
        };

        process.StartInfo = processStartInfo;
        process.Start();
        process.WaitForExit();

        _ = process.ExitCode;
    }

    public async Task Scan()
    {
        using var client = new RestClient("https://www.virustotal.com/vtapi/v2/file/scan");
        var request = new RestRequest
        {
            Method = Method.Post
        };

        request.AddHeader("Accept", "text/plain");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter(
            "application/x-www-form-urlencoded", 
            "apikey=6a5919038e44cbbd3e5a1c3736eab8e49f3768de363ab5add8ccdfb123100d4c", 
            ParameterType.RequestBody);

        request.AddFile("file", "", "multipart/form-data");
        var response = await client.ExecuteAsync(request);


        //using var client = new RestClient("https://www.virustotal.com/vtapi/v2/file/scan");
        //var virustotalRequest = new RestRequest();
        //virustotalRequest.AddHeader("Accept", "text/plain");
        //virustotalRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //virustotalRequest.AddParameter("apikey",
        //    _scanInfoService.VirusTotalApi, ParameterType.RequestBody);


        //var dsa = Uri.EscapeDataString(incident.FullPath);
        //virustotalRequest.AddFile("file", incident.FullPath, "application/x-www-form-urlencoded");
        //var response = await client.ExecutePostAsync(virustotalRequest);
    }

    public void EscalateSecurityIncident()
    {
        throw new NotImplementedException();
    }
}
