using System;
using System.Collections.Generic;

namespace LabWork20.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public decimal Price { get; set; }

    public DateTime DateTime { get; set; }

    public bool Is3D { get; set; }

    public int FilmId { get; set; }

    public byte HallId { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
