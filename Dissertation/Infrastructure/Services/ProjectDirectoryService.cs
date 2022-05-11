using Dissertation.Common.Services.DirectoryService;

namespace Dissertation.Infrastructure.Services;

public class ProjectDirectoryService : BaseDirectoryService
{
    public ProjectDirectoryService()
         : base(Path.GetFullPath(
                 Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
              new DirectoryInfo(Path.GetFullPath(
                 Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."))).Name)
    { }

    public ProjectDirectoryService(string filesDirectoryName)
        : base(Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
              filesDirectoryName)
    { }
}
