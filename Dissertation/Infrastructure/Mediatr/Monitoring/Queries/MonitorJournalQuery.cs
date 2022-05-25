using MediatR;
using System.Diagnostics;
using System.Text;

namespace Dissertation.Infrastructure.Mediatr.Monitoring.Queries;

public class MonitorJournalQuery : IRequest<string>
{
}

public class MonitorJournalQueryHandler : IRequestHandler<MonitorJournalQuery, string>
{
    public Task<string> Handle(MonitorJournalQuery request, CancellationToken cancellationToken)
    {
        var logs = EventLog.GetEventLogs();
        var result = new StringBuilder();

        var task = new Task(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                var collection = logs[i].Entries;

                foreach (EventLogEntry entry in collection)
                {
                    result.AppendFormat("0} - {1} - {2}",
                        entry.TimeGenerated,
                        entry.Source,
                        entry.Message)
                    .AppendLine();
                }
            }

            EventLog eventLog = new EventLog();
            foreach (EventLogEntry entry in eventLog.Entries)
            {
                
            }

        });

        task.Start();
        task.Wait(cancellationToken);

        return Task.FromResult(result.ToString());
    }
}
