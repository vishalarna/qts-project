using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class EvalResponse
{
    public decimal FkEval { get; set; }

    public decimal FkItem { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal FkEvalEvent { get; set; }

    public decimal FkLearningEvent { get; set; }

    public decimal? Response { get; set; }

    public string Feedback { get; set; }

    public decimal? Type { get; set; }

    public virtual EvalEvent FkEvalEventNavigation { get; set; }

    public virtual Eval FkEvalNavigation { get; set; }

    public virtual EvalItem FkItemNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
