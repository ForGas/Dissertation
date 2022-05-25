using MediatR;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;

namespace Dissertation.Infrastructure.Mediatr.Monitoring.Queries;

public class MonitorNetworkQuery : IRequest<string>
{
}

public class MonitorQueryHandler : IRequestHandler<MonitorNetworkQuery, string>
{
    public Task<string> Handle(MonitorNetworkQuery request, CancellationToken cancellationToken)
    {
        var performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
        var instance = performanceCounterCategory.GetInstanceNames()[0];
        var performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
        var performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);

        var result = new StringBuilder();

        var task = new Task(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                result.AppendFormat("bytes sent: {0}k\tbytes received: {1}k",
                performanceCounterSent.NextValue(),
                performanceCounterReceived.NextValue())
                    .AppendLine();

                Thread.Sleep(40);
            }
        });

        task.Start();

        task.Wait(cancellationToken);

        return Task.FromResult(result.ToString());
    }

    public void Test()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
            return;

        NetworkInterface[] interfaces
            = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface ni in interfaces)
        {
            Console.WriteLine("    Bytes Sent: {0}",
                ni.GetIPv4Statistics().BytesSent);
            Console.WriteLine("    Bytes Received: {0}",
                ni.GetIPv4Statistics().BytesReceived);
        }
    }
}
