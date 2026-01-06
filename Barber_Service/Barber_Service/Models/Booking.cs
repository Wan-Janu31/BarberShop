using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int BarberId { get; set; }

    public decimal TotalAmount { get; set; }

    public string BookingStatus { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Note { get; set; }

    public virtual Barber Barber { get; set; } = null!;

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
