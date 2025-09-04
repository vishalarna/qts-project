using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class XrefLibImpl
{
    public decimal Id { get; set; }

    public decimal FkXrefLib { get; set; }

    public decimal FkProject { get; set; }

    public string Text { get; set; }

    public string TextSort { get; set; }

    public decimal? FkParent { get; set; }

    public decimal IsOrganizer { get; set; }

    public string UserDefinedId { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public string TextAscii { get; set; }

    public string VersionComments { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual XrefLib FkParentNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual XrefLib FkXrefLibNavigation { get; set; }

    public virtual ICollection<RevisionLogXref> RevisionLogXrefs { get; set; } = new List<RevisionLogXref>();
}
