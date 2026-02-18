using System;
using System.Collections.Generic;

namespace TuristaAPI.Models;

public partial class Nehezseg
{
    public int Id { get; set; }

    public string Jelzes { get; set; } = null!;

    public string Leiras { get; set; } = null!;

    public virtual ICollection<Utvonal>? Utvonals { get; set; } = new List<Utvonal>();
}
