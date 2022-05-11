using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities.Common;

public abstract class Respondent : BaseModel
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
}
