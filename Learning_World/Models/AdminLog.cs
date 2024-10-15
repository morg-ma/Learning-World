using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class AdminLog
{
    public int LogId { get; set; }

    public int? AdminId { get; set; }

    public string ActionType { get; set; } = null!;

    public string ActionDescription { get; set; } = null!;

    public DateTime ActionDate { get; set; }

    public virtual User? Admin { get; set; }
}
