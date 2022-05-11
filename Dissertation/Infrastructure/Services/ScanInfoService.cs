using Dissertation.Common.Services;

namespace Dissertation.Infrastructure.Services;

public class ScanInfoService : IScanInfoService
{
    public string FileStorageFolderName => "Files";
    public string AntivirusScanInSystemCommand => "C:/Program Files/Windows Defender/MpCmdRun.exe";
    public string VirusTotalApiKey => "6a5919038e44cbbd3e5a1c3736eab8e49f3768de363ab5add8ccdfb123100d4c";
    public string VirusTotalScanUrl => "https://www.virustotal.com/vtapi/v2/file/scan";
    public string VirusTotalReportUrl => "https://www.virustotal.com/vtapi/v2/file/report";
}
