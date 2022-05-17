﻿using Dissertation.Common.Services.CSIRP;
using Dissertation.Persistence.Entities.Base;

namespace Dissertation.Persistence.Entities;

/// <summary>План реагирования на инциденты</summary>
public class PatternePlan : BaseIdentity, IPlan
{
    public string Content { get; set; } = null!;
    //public virtual ResponseToolInfo? ResponseToolInfo { get; set; }
    public string PathMap { get; set; } = null!;
    public TimeSpan Duration { get; set; }
}
