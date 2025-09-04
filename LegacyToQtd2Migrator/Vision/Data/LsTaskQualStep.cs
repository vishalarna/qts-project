using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTaskQualStep
{
    public decimal Id { get; set; }

    public decimal FkTaskQualification { get; set; }

    public decimal FkAnalysis { get; set; }

    public decimal? FkEvaluator { get; set; }

    public decimal Status { get; set; }

    public string EvaluatorComments { get; set; }

    public string TextAscii { get; set; }

    public decimal Archive { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual Learner FkEvaluatorNavigation { get; set; }

    public virtual LsTaskQualification FkTaskQualificationNavigation { get; set; }

    public virtual LsStatus StatusNavigation { get; set; }
}
