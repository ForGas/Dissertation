using Dissertation.Common.Services.CSIRP;

namespace Dissertation.Persistence.Entities;

public class Plan : IPlan
{
    public string PathMap { get; set; }
    public TimeSpan Duration { get; set; }
    public string Content { get; set; }
}
