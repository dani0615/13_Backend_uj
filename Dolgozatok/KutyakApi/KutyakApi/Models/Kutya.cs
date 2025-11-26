using System;
using System.Collections.Generic;

namespace KutyakApi.Models;

public partial class Kutya
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Fajta { get; set; } = null!;

    public int Marmagassag { get; set; }

    public int Tomeg { get; set; }

    public byte[] Kep { get; set; } = null!;

    public int GazdaId { get; set; }

    public virtual Gazdum? Gazda { get; set; } = null!;
}
