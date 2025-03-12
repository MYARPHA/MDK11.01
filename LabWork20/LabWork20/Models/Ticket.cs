using System;
using System.Collections.Generic;

namespace LabWork20.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public byte Place { get; set; }

    public byte Row { get; set; }

    public int SessionId { get; set; }

    public int VisitorId { get; set; }

    public virtual Session Session { get; set; } = null!;
}
