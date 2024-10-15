using System;
using System.Collections.Generic;

namespace Learning_World.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
