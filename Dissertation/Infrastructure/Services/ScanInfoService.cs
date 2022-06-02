using Dissertation.Common.Services;

namespace Dissertation.Infrastructure.Services;

public class ScanInfoService : IScanInfoService
{
    public string FileStorageFolderName => "Files";
    public string AntivirusScanInSystemCommand => "C:/Program Files/Windows Defender/MpCmdRun.exe";
    public string VirusTotalApiKey => "";
    public string VirusTotalScanUrl => "https://www.virustotal.com/vtapi/v2/file/scan";
    public string VirusTotalReportUrl => "https://www.virustotal.com/vtapi/v2/file/report";
}
