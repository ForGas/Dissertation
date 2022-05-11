using MediatR;
using Dissertation.Common.Services;
using System.Reflection;
using Dissertation.Infrastructure.Services;
using Dissertation.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Dissertation.Common.Services.DirectoryService;
using Hangfire;
using Hangfire.SqlServer;
using Dissertation.Infrastructure.Common;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Newtonsoft.Json;
using Microsoft.AspNetCore.HttpOverrides;

namespace Dissertation;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        string hfDbConnection = Configuration.GetConnectionString("HangfireDbConnection");
        services.AddDbContext<HangfireDbContext>(opt => opt.UseSqlServer(hfDbConnection));

        services.AddHangfire(configuration =>
                    configuration
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseSqlServerStorage(hfDbConnection, new SqlServerStorageOptions
                            {
                                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                QueuePollInterval = TimeSpan.Zero,
                                UseRecommendedIsolationLevel = true,
                                DisableGlobalLocks = true
                            }));

        services.AddHangfireServer();

        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IApplicationDbContext>(provider => provider
            .GetService<ApplicationDbContext>() ?? throw new Exception());

        services.Configure<EmailHostSettings>(Configuration.GetSection("EmailHostSettings"));

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IScanInfoService, ScanInfoService>();
        services.AddTransient<IFileService, ProjectDirectoryService>();
        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IApplicationDbContext>(provider => provider
            .GetService<ApplicationDbContext>());

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dissertation", Version = "v1" });
        });

        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = @"../clientapp/build";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dissertation v1"));
        }

        app.UseForwardedHeaders();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseStaticFiles();
        app.UseSpaStaticFiles();

        app.UseAuthorization();

        app.UseHangfireDashboard("/hangfire");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            endpoints.MapHangfireDashboard();
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = @"../clientapp";

            if (env.IsDevelopment())
            {
                //spa.UseReactDevelopmentServer(npmScript: "start");
                spa.UseProxyToSpaDevelopmentServer("https://localhost:3000");
            }
        });
    }
}
