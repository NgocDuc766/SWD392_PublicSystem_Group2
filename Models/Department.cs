using System;
using System.Collections.Generic;

namespace SWD392_PublicService.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<PublicService> PublicServices { get; set; } = new List<PublicService>();
}
