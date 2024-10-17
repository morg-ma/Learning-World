using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Course
{
    public int CourseId { get; set; }
    public decimal Price { get; set; } // Add Price property
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string DifficultyLevel { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string PublicationStatus { get; set; } = null!;

    public int? MaxEnrollment { get; set; }

    public decimal? AverageRating { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
