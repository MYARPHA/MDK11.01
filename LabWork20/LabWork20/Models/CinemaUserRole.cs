using System;
using System.Collections.Generic;

namespace LabWork20.Models;

public partial class CinemaUserRole
{
    public int UserRoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<CinemaUser> CinemaUsers { get; set; } = new List<CinemaUser>();
}
