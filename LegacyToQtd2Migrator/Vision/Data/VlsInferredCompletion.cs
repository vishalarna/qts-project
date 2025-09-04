using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsInferredCompletion
{
    public decimal? Id { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal? FkLearner { get; set; }

    public decimal? FkLearningEvent { get; set; }

    public decimal? FkQualEvent { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateCompleted { get; set; }

    public decimal Status { get; set; }

    public DateTime? DateExpires { get; set; }

    public decimal? Exempt { get; set; }

    public string ExemptComments { get; set; }

    public decimal? Archive { get; set; }

    public string Comments { get; set; }

    public decimal? FkCatalog { get; set; }

    public decimal? ActualCourse { get; set; }

    public int ActualType { get; set; }
}
