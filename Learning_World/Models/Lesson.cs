using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int? ModuleId { get; set; }

    public int? LessonTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderInModule { get; set; }

    public virtual LessonQuiz? LessonQuiz { get; set; }

    public virtual LessonText? LessonText { get; set; }

    public virtual LessonType? LessonType { get; set; }

    public virtual LessonVideo? LessonVideo { get; set; }

    public virtual Module? Module { get; set; }

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}
