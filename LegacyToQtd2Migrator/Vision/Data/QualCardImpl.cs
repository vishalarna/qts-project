using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QualCardImpl
{
    public decimal Id { get; set; }

    public decimal FkQualCard { get; set; }

    public decimal FkProject { get; set; }

    public string Text { get; set; }

    public decimal? FkTimeToComplete { get; set; }

    public decimal? FkQualCardStatus { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal? FkProgram { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual QualCard FkQualCardNavigation { get; set; }

    public virtual QualCardStatus FkQualCardStatusNavigation { get; set; }

    public virtual LsTimeToComplete FkTimeToCompleteNavigation { get; set; }

    public virtual ICollection<LsQualCard> LsQualCards { get; set; } = new List<LsQualCard>();

    public virtual ICollection<RevisionLogQualCard> RevisionLogQualCards { get; set; } = new List<RevisionLogQualCard>();
}
