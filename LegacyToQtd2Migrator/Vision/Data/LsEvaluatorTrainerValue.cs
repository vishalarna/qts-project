using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsEvaluatorTrainerValue
{
    public decimal IdValue { get; set; }

    public decimal Optvalue { get; set; }

    public string Text { get; set; }

    public decimal Sequence { get; set; }

    public decimal IsTrainerType { get; set; }

    public decimal IsEvaluatorType { get; set; }

    public virtual ICollection<LsPerformanceAssessment> LsPerformanceAssessmentEvaluatorTypeNavigations { get; set; } = new List<LsPerformanceAssessment>();

    public virtual ICollection<LsPerformanceAssessment> LsPerformanceAssessmentTrainerTypeNavigations { get; set; } = new List<LsPerformanceAssessment>();

    public virtual ICollection<LsQualCard> LsQualCardEvaluatorTypeNavigations { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsQualCard> LsQualCardTrainerTypeNavigations { get; set; } = new List<LsQualCard>();
}
