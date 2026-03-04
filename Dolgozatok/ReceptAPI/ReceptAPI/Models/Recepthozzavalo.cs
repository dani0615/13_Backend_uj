using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Recepthozzavalo
{
    public int ReceptId { get; set; }

    public int HozzavaloId { get; set; }

    public int Mennyiseg { get; set; }

    public virtual Hozzavalo Hozzavalo { get; set; } = null!;

    public virtual Recept Recept { get; set; } = null!;
}
