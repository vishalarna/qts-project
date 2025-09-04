using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EvalType
{
    public decimal EvalTypeId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal FkRatingStyleType { get; set; }

    public decimal FkRatingUseType { get; set; }

    public decimal Isactive { get; set; }

    public string ItemLabel { get; set; }

    public virtual ICollection<EvalRatingItem> EvalRatingItems { get; set; } = new List<EvalRatingItem>();

    public virtual ICollection<LsProfileEvalType> LsProfileEvalTypeClrs { get; set; } = new List<LsProfileEvalType>();

    public virtual ICollection<LsProfileEvalType> LsProfileEvalTypeRatingScales { get; set; } = new List<LsProfileEvalType>();

    public virtual ICollection<LsProfileEvalType> LsProfileEvalTypeReasonCodes { get; set; } = new List<LsProfileEvalType>();
}
