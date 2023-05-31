using System;
using System.Collections.Generic;

namespace LegendApi.Models;

public partial class Animal
{
    public int Id { get; set; }

    public int? Animaltype { get; set; }

    public float? Weight { get; set; }

    public float? Lenght { get; set; }

    public float? Height { get; set; }

    public string? Gender { get; set; }

    public string? Lifestatus { get; set; }

    public DateTime? Chippingdatetime { get; set; }

    public int? Chipperid { get; set; }

    public int? Chippinglocationid { get; set; }

    public int? Visitedlocations { get; set; }

    public DateTime? Deathdatetime { get; set; }

    public virtual Typeanimal? AnimaltypeNavigation { get; set; }

    public virtual User? Chipper { get; set; }

    public virtual Location? Chippinglocation { get; set; }

    public virtual ICollection<Historyvisitlocation> Historyvisitlocations { get; set; } = new List<Historyvisitlocation>();
}
