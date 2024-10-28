using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class ApplicationDocument
{
    public int ApplicationId { get; set; }

    public int DocumentId { get; set; }

    public DateTime? AttachDate { get; set; }

    public string? Description { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
