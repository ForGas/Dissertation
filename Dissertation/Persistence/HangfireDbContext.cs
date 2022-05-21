using Microsoft.EntityFrameworkCore;

namespace Dissertation.Persistence;

public class HangfireDbContext : DbContext
{
    public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options) { }
}
