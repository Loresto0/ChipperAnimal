using System;
using System.Collections.Generic;

namespace LegendApi.Models;

public partial class Historyvisitlocation
{
    public int Id { get; set; }

    public int? Idanimal { get; set; }

    public int? Locationpointid { get; set; }

    public DateTime? Datetimeofvisitlocationpoint { get; set; }

    public virtual Animal? IdanimalNavigation { get; set; }

    public virtual Location? Locationpoint { get; set; }
}
