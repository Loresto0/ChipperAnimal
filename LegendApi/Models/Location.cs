using System;
using System.Collections.Generic;

namespace LegendApi.Models;

public partial class Location
{
    public int Id { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<Historyvisitlocation> Historyvisitlocations { get; set; } = new List<Historyvisitlocation>();
}
