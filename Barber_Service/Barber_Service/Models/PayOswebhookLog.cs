using System;
using System.Collections.Generic;

namespace Barber_Service.Models;

public partial class PayOswebhookLog
{
    public int Id { get; set; }

    public long OrderCode { get; set; }

    public string Event { get; set; } = null!;

    public string RawData { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }
}
