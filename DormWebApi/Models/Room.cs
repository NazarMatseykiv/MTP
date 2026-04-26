using System;
using System.Collections.Generic;

namespace DormWebApi.Models;

public partial class Room
{
    public int Id { get; set; }

    public string? RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();
}
