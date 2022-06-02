using Dissertation.Persistence;
using Serilog;

namespace Dissertation;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            Log.Information("Starting web host");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.WriteTo.Console();
            });

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();
            await DbInitializer.InitializeAsync(app.Services);

            startup.Configure(app, app.Environment);
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
