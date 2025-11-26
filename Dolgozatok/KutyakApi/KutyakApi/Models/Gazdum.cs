using System;
using System.Collections.Generic;

namespace KutyakApi.Models;

public partial class Gazdum
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Cim { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Kutya>? Kutyas { get; set; } = new List<Kutya>();
}
