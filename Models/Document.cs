using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public string? Name { get; set; }

    public string? Path { get; set; }

    public int? Type { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<ApplicationDocument> ApplicationDocuments { get; set; } = new List<ApplicationDocument>();

    public virtual User? CreatedByNavigation { get; set; }
}
