using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSecurityCode
{
    public decimal IdValue { get; set; }

    public string SecurityAccess { get; set; }

    public string Comments { get; set; }

    public DateTime DateBegin { get; set; }

    public DateTime? DateExpire { get; set; }

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();

    public virtual ICollection<LsLearningEventLearner> LsLearningEventLearners { get; set; } = new List<LsLearningEventLearner>();
}
