using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EvalRatingItem
{
    public decimal Itemid { get; set; }

    public string Name { get; set; }

    public decimal? PointValue { get; set; }

    public decimal? FkEvalTypes { get; set; }

    public string Description { get; set; }

    public decimal? Fail { get; set; }

    public decimal? Isactive { get; set; }

    public virtual EvalType FkEvalTypesNavigation { get; set; }
}
