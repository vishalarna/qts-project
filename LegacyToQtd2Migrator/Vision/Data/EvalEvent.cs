using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EvalEvent
{
    public decimal Evalid { get; set; }

    public decimal? FkEvent { get; set; }

    public decimal? FkEval { get; set; }

    public decimal? FkLearner { get; set; }

    public decimal? FkProgram { get; set; }

    public DateTime? CompDate { get; set; }

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual Eval FkEvalNavigation { get; set; }

    public virtual LsLearningEvent FkEventNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
