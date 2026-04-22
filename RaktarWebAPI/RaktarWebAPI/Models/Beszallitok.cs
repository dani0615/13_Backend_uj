using System;
using System.Collections.Generic;

namespace RaktarWebAPI.Models;

public partial class Beszallitok
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Cim { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Termekek> Termekeks { get; set; } = new List<Termekek>();
}
