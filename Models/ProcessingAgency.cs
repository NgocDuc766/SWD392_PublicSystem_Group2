using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class ProcessingAgency
{
    public int AgencyId { get; set; }

    public int? Level { get; set; }

    public int? Type { get; set; }

    public string? District { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
