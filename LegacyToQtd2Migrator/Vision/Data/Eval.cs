using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Eval
{
    public decimal Id { get; set; }

    public string Name { get; set; }

    public decimal Type { get; set; }

    public decimal? Status { get; set; }

    public virtual ICollection<EvalEvent> EvalEvents { get; set; } = new List<EvalEvent>();

    public virtual ICollection<EvalItem> EvalItems { get; set; } = new List<EvalItem>();

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual ICollection<ExamOnlineProfile> ExamOnlineProfiles { get; set; } = new List<ExamOnlineProfile>();

    public virtual ICollection<LsLearningEventProgram> LsLearningEventPrograms { get; set; } = new List<LsLearningEventProgram>();

    public virtual ICollection<LsLearningEvent> LsLearningEvents { get; set; } = new List<LsLearningEvent>();
}
