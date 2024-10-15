using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Progress
{
    public int ProgressId { get; set; }

    public int? UserId { get; set; }

    public int? LessonId { get; set; }

    public bool CompletionStatus { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Lesson? Lesson { get; set; }

    public virtual User? User { get; set; }
}
