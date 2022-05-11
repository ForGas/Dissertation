using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dissertation.Persistence;

public class HangfireDbContext : DbContext
{
    public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options) { }
}
