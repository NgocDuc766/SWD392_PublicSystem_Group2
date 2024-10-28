using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class PublicService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public decimal? ServiceFee { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateDeleted { get; set; }

    public int? IsDeleted { get; set; }

    public int? ProcessedBy { get; set; }

    public int? DepartmentId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Department? Department { get; set; }
}
