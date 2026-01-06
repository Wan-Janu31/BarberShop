using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public string Provider { get; set; } = null!;

    public long OrderCode { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string CheckoutUrl { get; set; } = null!;

    public string? TransactionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
