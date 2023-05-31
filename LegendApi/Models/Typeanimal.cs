using System;
using System.Collections.Generic;

namespace LegendApi.Models;

public partial class Typeanimal
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
