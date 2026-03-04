using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Nehezseg
{
    public int Id { get; set; }

    public string Szint { get; set; } = null!;

    public string Leiras { get; set; } = null!;

    public virtual ICollection<Recept> Recepts { get; set; } = new List<Recept>();
}
