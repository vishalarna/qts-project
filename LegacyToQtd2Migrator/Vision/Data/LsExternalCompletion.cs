using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExternalCompletion
{
    public decimal Id { get; set; }

    public string Pin { get; set; }

    public string CourseCode { get; set; }

    public DateTime CompletionDate { get; set; }

    public string Credit { get; set; }

    public decimal Xfer { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateXfer { get; set; }

    public string Source { get; set; }

    public string XferMessage { get; set; }

    public decimal? FkLearner { get; set; }

    public decimal? FkProgram { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
