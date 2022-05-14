using Dissertation.Persistence.Entities.Base;

#nullable disable
namespace Dissertation.Persistence.Entities.Common;

public abstract class Respondent : BaseIdentity
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
}
