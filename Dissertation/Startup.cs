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
using Newtonsoft.Json;
using Microsoft.AspNetCore.HttpOverrides;
using FluentValidation.AspNetCore;
using FluentValidation;
using Dissertation.Common.Services.CSIRP;
using Dissertation.Infrastructure.Services.CSIRP;

#nullable disable
namespace Dissertation;

public class Startup
{
    public Startup(IConfiguration configuration) => (Configuration) = (configuration);

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }).AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            fv.ImplicitlyValidateRootCollectionElements = true;
            fv.AutomaticValidationEnabled = false;
        });

        services.Configure<ForwardedHeadersOptions>(options =>
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        );

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

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

        //services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddCors(c =>
            c.AddPolicy("AllowOrigin", options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader())
        );

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.Configure<EmailHostSettings>(Configuration.GetSection("EmailHostSettings"));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IComputerSecurityIncidentResponsePlan<IIncident>, ComputerSecurityIncidentResponsePlan>();
        services.AddTransient<IPlanService, PlanService>();
        services.AddTransient<IScanInfoService, ScanInfoService>();
        services.AddTransient<IFileService, ProjectDirectoryService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IRespondentAutomationLogic, RespondentPredictionAutomate>();

        services.AddScoped(x => new RespondentPredictionAutomate(
                    x.GetService<IApplicationDbContext>()));

        services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dissertation", Version = "v1" })
        );
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
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.UseHangfireDashboard("/hangfire");
        app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            endpoints.MapHangfireDashboard();
        });

        RecurringJob.AddOrUpdate<AutomateStaffStatisticWorkloadJobService>(job => job.AutomateAsync(), "*/5 * * * *", TimeZoneInfo.Local);
    }
}
