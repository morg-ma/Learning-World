﻿using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
