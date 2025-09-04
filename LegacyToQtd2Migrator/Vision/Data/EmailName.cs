using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EmailName
{
    public decimal Id { get; set; }

    public string EmailName1 { get; set; }

    public decimal FkEmailCategory { get; set; }

    public string Description { get; set; }

    public decimal Enabled { get; set; }

    public virtual EmailCategory FkEmailCategoryNavigation { get; set; }
}
