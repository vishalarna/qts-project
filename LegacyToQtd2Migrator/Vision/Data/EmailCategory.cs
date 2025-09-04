using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EmailCategory
{
    public decimal Id { get; set; }

    public string EmailCategory1 { get; set; }

    public virtual ICollection<EmailName> EmailNames { get; set; } = new List<EmailName>();
}
