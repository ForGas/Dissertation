namespace Dissertation.Common.Services;

public interface IScanInfoService
{
    string FileStorageFolderName { get; }
    string AntivirusScanInSystemCommand { get; }
    string VirusTotalApiKey { get; }
    string VirusTotalScanUrl { get; }
    string VirusTotalReportUrl { get; }
}
