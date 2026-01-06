using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class Barber
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public int ExperienceYears { get; set; }

    public string Phone { get; set; } = null!;

    public string? Description { get; set; }

    public double RatingAvg { get; set; }

    public int RatingCount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
