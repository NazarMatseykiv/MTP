using System;
using System.Collections.Generic;

namespace DormWebApi.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Faculty { get; set; }

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();
}
