using System;
using System.Collections.Generic;

namespace DormWebApi.Models;

public partial class Settlement
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Student? Student { get; set; }
}
