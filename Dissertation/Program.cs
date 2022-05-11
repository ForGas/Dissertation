using Dissertation.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dissertation;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            Log.Information("Starting web host");

            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            var hcontext = services.GetRequiredService<HangfireDbContext>();

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
                hcontext.Database.Migrate();
            }

            await host.RunAsync();
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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
