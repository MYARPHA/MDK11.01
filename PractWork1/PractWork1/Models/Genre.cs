using System;
using System.Collections.Generic;

namespace PractWork1.Models;

public partial class Genre
{
    public string Genre1 { get; set; } = null!;

    public int GenreId { get; set; }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
