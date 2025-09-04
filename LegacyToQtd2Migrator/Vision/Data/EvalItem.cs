using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EvalItem
{
    public decimal StemId { get; set; }

    public decimal FkEval { get; set; }

    public string Stem { get; set; }

    public decimal? ResponseType { get; set; }

    public decimal? Sequence { get; set; }

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual Eval FkEvalNavigation { get; set; }
}
