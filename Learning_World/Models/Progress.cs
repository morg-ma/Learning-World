using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Progress
{
    public int ProgressId { get; set; }

    public int? UserId { get; set; }

    public int? PartId { get; set; }

    public bool CompletionStatus { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Part? Part { get; set; }

    public virtual User? User { get; set; }
}
