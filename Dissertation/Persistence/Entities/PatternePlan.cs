using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Base;
using System;

namespace Dissertation.Persistence.Entities;

/// <summary>План реагирования на инциденты</summary>
public class PatternePlan : BaseModel, IPlan
{
    public string Content { get; set; }
    //public virtual ResponseToolInfo? ResponseToolInfo { get; set; }
    public string PathMap { get; set; }
    public TimeSpan Duration { get; set; }
}
