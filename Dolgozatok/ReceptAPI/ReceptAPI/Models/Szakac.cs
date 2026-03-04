using System;
using System.Collections.Generic;

namespace ReceptAPI.Models;

public partial class Szakac
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefonszam { get; set; } = null!;

    public virtual ICollection<Recept> Recepts { get; set; } = new List<Recept>();
}
