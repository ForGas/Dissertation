namespace Dissertation.Common.Services.DirectoryService;

public interface IFileService : IPath, ICheckFile
{
    public List<string> FindFilesNameByExtension(string approximateFileName);
    public string GetExtensionByFileName(string fileName);
}
