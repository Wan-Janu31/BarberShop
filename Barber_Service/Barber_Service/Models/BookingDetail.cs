using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class BookingDetail
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int TimeSlotId { get; set; }

    public decimal Price { get; set; }

    public string Status { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;

    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
