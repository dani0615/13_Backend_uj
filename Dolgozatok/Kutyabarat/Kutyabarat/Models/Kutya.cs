using System;
using System.Collections.Generic;

namespace Kutyabarat.Models;

public partial class Kutya
{
    public int Id { get; set; }

    public string Fajta { get; set; } = null!;

    public int Marmagassag { get; set; }

    public int Tomeg { get; set; }

    public byte[] Kep { get; set; } = null!;
}
