using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public int? Type { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public string? Note { get; set; }

    public int? UserId { get; set; }

    public int? ServiceId { get; set; }

    public int? AgencyId { get; set; }

    public virtual ProcessingAgency? Agency { get; set; }

    public virtual ICollection<ApplicationDocument> ApplicationDocuments { get; set; } = new List<ApplicationDocument>();

    public virtual PublicService? Service { get; set; }

    public virtual User? User { get; set; }
}
