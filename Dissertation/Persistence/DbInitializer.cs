using Microsoft.EntityFrameworkCore;
using Dissertation.Persistence.Entities;
using Dissertation.Persistence.Entities.Common;

namespace Dissertation.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        var hcontext = services.GetRequiredService<HangfireDbContext>();

        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
            hcontext.Database.Migrate();

            await SeedStaffAsync(context);
            await SeedStaffStatisticAsync(context);
            await SeedPlansAsync(context);
        }
    }

    private static async Task SeedStaffAsync(ApplicationDbContext context)
    {
        var staffs = new List<Staff>{
            new Staff {
                Type = StaffType.CyberSecuritySpecialist,
                Email = "CyberSecuritySpecialist1@gmail.com",
                FirstName = "CyberSecuritySpecialist1",
                LastName = "CyberSecuritySpecialist1",
                MiddleName = "CyberSecuritySpecialist1"
            },
            new Staff {
                Type = StaffType.CyberSecuritySpecialist,
                Email = "CyberSecuritySpecialist2@gmail.com",
                FirstName = "CyberSecuritySpecialist2",
                LastName = "CyberSecuritySpecialist2",
                MiddleName = "CyberSecuritySpecialist2"
            },
            new Staff {
                Type = StaffType.Analyst,
                Email = "Analyst1@gmail.com",
                FirstName = "Analyst1",
                LastName = "Analyst1",
                MiddleName = "Analyst1"
            },
            new Staff {
                Type = StaffType.Analyst,
                Email = "Analyst2@gmail.com",
                FirstName = "Analyst2",
                LastName = "Analyst2",
                MiddleName = "Analyst2"
            },
            new Staff {
                Type = StaffType.ServiceManager,
                Email = "ServiceManager1@gmail.com",
                FirstName = "ServiceManager1",
                LastName = "ServiceManager1",
                MiddleName = "ServiceManager1"
            },
             new Staff {
                Type = StaffType.Director,
                Email = "Director1@gmail.com",
                FirstName = "Director1",
                LastName = "Director1",
                MiddleName = "Director1"
            },
        };

        foreach (var staff in staffs)
        {
            if (!context.Staffs.Any(x => x.FirstName == staff.FirstName))
            {
                context.Staffs.Add(staff);
            }
        }

        _ = await context.SaveChangesAsync();
    }

    private static async Task SeedStaffStatisticAsync(ApplicationDbContext context)
    {
        var staffs = await context.Staffs.Include(x => x.Statistics)
            .Where(x => x.Statistics != null && x.Statistics.Count < 1
            && (x.Type == StaffType.CyberSecuritySpecialist || x.Type == StaffType.Analyst))
            .ToListAsync();

        var statistics = new List<StaffStatistic>();

        foreach (var staff in staffs)
        {
            var staffStatistics = new List<StaffStatistic>();

            staffStatistics.Add(new StaffStatistic()
            {
                StatisticsType = IncidentType.File,
                Workload = Workload.Neutral,
                IsRelevance = true
            });

            staffStatistics.Add(new StaffStatistic()
            {
                StatisticsType = IncidentType.Network,
                Workload = Workload.Neutral,
                IsRelevance = true
            });

            staff.Statistics.AddRange(staffStatistics);
            statistics.AddRange(staffStatistics);
        }

        await context.StaffStatistics.AddRangeAsync(statistics);
        _ = await context.SaveChangesAsync();
    }

    private static async Task SeedPlansAsync(ApplicationDbContext context)
    {
        var plans = new List<PlannedResponsePlan>{
            new PlannedResponsePlan {
                Type = PlanTypeStrategy.Pattern,
                Title = "Title-File-Low-PlannedResponsePlan",
                Performance = TimeSpan.FromMinutes(60),
                IncidentType = IncidentType.File,
                Priority = Priority.Low
            },
            new PlannedResponsePlan {
                Type = PlanTypeStrategy.Pattern,
                Title = "Title-File-Middle-PlannedResponsePlan",
                Performance = TimeSpan.FromMinutes(60),
                IncidentType = IncidentType.File,
                Priority = Priority.Middle
            },
            new PlannedResponsePlan {
                Type = PlanTypeStrategy.Pattern,
                Title = "Title-File-High-PlannedResponsePlan",
                Performance = TimeSpan.FromMinutes(60),
                IncidentType = IncidentType.File,
                Priority = Priority.High
            },
        };

        foreach (var plan in plans)
        {
            if (!context.PlannedResponsePlans.Any(x => x.Title == plan.Title))
            {
                var pathMapContents = new List<PathMapContent>
                {
                    new PathMapContent()
                    {
                        Title = "Title-Analysis-PathMapContent",
                        ResponseToolInfo = "ResponseToolInfo-Analysis",
                        Source = "Source-Analysis",
                        Stage = PathMapStage.Analysis,
                        Description = "Description-Analysis"
                    },
                    new PathMapContent()
                    {
                        Title = "Title-Define-PathMapContent",
                        ResponseToolInfo = "ResponseToolInfo-Define",
                        Source = "Source-Define",
                        Stage = PathMapStage.Define,
                        Description = "Description-Define"
                    },
                    new PathMapContent()
                    {
                        Title = "Title-Escalate-PathMapContent",
                        ResponseToolInfo = "ResponseToolInfo-Escalate",
                        Source = "Source-Escalate",
                        Stage = PathMapStage.Escalate,
                        Description = "Description-Escalate"
                    },
                    new PathMapContent()
                    {
                        Title = "Title-Complete-PathMapContent",
                        ResponseToolInfo = "ResponseToolInfo-Complete",
                        Source = "Source-Complete",
                        Stage = PathMapStage.Complete,
                        Description = "Description-Complete"
                    },
                };

                plan.PathMaps.AddRange(pathMapContents);
                context.PlannedResponsePlans.Add(plan);
                context.PathMapContents.AddRange(pathMapContents);
            }
        }

        _ = await context.SaveChangesAsync();
    }
}
