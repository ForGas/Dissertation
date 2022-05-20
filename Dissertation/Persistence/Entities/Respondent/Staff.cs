using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

public class Staff : BaseIdentity
{
    public StaffType Type { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual List<StaffStatistic> Statistics { get; set; } = new();
}
