namespace Dissertation.Common.Services.DirectoryService;

#nullable disable
public abstract class BaseDirectoryService : IFileService
{
    protected readonly string _directoryPath;
    protected readonly string _filesDirectoryName;

    protected BaseDirectoryService(string directory, string filesDirectoryName)
        => (_directoryPath, _filesDirectoryName) = (directory, filesDirectoryName);

    public string GetDirectoryPath() => _directoryPath;
 
    public string GetFilePath(string fileName)
    {
        return string.IsNullOrEmpty(fileName)
            ? null
            : (_filesDirectoryName == new DirectoryInfo(_directoryPath).Name
                ? string.Concat(_directoryPath, '\\', fileName)
                : Path.GetFullPath(Path.Combine(_filesDirectoryName, fileName), _directoryPath));
    }

    public string GetExtensionByFileName(string fileName) 
        => Path.GetExtension(fileName);

    public bool IsFileExists(string fileName)
        => File.Exists(GetFilePath(fileName));

    public List<string> FindFilesNameByExtension(string approximateFileName)
    {
        return !string.IsNullOrEmpty(approximateFileName)
            ? null
            : ((Func<List<string>>)(() =>
            {
                var filesNames = Directory
                        .GetFiles(Path.Combine(_directoryPath, _filesDirectoryName))
                        .Select(Path.GetFileName)
                        .ToList();

                var extension = GetExtensionByFileName(approximateFileName);

                return filesNames.Where(x => extension == GetExtensionByFileName(x)).ToList();
            })).Invoke();
    }
}
