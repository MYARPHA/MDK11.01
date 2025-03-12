using System;
using System.Collections.Generic;

namespace LabWork20.Models;

public partial class CinemaUser
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int UserRoleId { get; set; }

    public virtual CinemaUserRole UserRole { get; set; } = null!;
}
