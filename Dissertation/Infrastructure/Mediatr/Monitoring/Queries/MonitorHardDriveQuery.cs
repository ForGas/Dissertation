using MediatR;

namespace Dissertation.Infrastructure.Mediatr.Monitoring.Queries;

public class MonitorHardDriveQuery : IRequest
{
}

public class MonitorHardDriveQueryHandler : IRequestHandler<MonitorHardDriveQuery, Unit>
{
    public Task<Unit> Handle(MonitorHardDriveQuery request, CancellationToken cancellationToken)
    {
        var watchers = new FileSystemWatcher[] {};

        string[] drives = Environment.GetLogicalDrives();

        watchers = new FileSystemWatcher[drives.Length];

        int i = 0;

        //foreach (string strDrive in drives)

        //{
        //    var localWatcher = new FileSystemWatcher();

        //    localWatcher.Path = strDrive;

        //    watchers[i] = localWatcher;

        //    localWatcher.EnableRaisingEvents = true;
        //    i++;
        //}

        return Task.FromResult(Unit.Value);
    }
}
