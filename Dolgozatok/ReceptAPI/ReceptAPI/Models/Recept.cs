using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Recept
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Leiras { get; set; } = null!;

    public int ElkeszitesiIdo { get; set; }

    public int NehezsegId { get; set; }

    public int SzakacsId { get; set; }

    public virtual Nehezseg Nehezseg { get; set; } = null!;

    public virtual ICollection<Recepthozzavalo> Recepthozzavalos { get; set; } = new List<Recepthozzavalo>();

    public virtual Szakac Szakacs { get; set; } = null!;
}
