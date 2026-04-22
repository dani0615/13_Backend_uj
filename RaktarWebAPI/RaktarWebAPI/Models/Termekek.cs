using System;
using System.Collections.Generic;

namespace RaktarWebAPI.Models;

public partial class Termekek
{
    public int Id { get; set; }

    public string Megnevezes { get; set; } = null!;

    public int Ar { get; set; }

    public int BeszallitoId { get; set; }

    public string Leiras { get; set; } = null!;

    public virtual Beszallitok Beszallito { get; set; } = null!;
}
