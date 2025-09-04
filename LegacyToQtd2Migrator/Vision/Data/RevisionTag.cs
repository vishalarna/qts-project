using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionTag
{
    public decimal Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime? TagDatetime { get; set; }

    public decimal? FkProject { get; set; }

    public decimal? Archived { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
