using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class TimeSlot
{
    public int Id { get; set; }

    public int BarberId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Barber Barber { get; set; } = null!;

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
