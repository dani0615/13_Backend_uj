using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Hozzavalo
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public int Kaloria { get; set; }

    public virtual ICollection<Recepthozzavalo> Recepthozzavalos { get; set; } = new List<Recepthozzavalo>();
}
