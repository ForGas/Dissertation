namespace Dissertation.Common.Services.DirectoryService;

public interface IPath
{
    public string? GetFilePath(string fileName);
    public string GetDirectoryPath();
}
