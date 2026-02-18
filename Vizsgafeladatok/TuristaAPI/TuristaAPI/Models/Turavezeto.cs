using System;
using System.Collections.Generic;

namespace TuristaAPI.Models;

public partial class Turavezeto
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Minosites { get; set; }

    public virtual ICollection<Tura>? Turas { get; set; } = new List<Tura>();
}
