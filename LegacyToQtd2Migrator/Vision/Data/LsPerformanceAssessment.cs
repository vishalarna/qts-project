using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPerformanceAssessment
{
    public decimal FkCatalogLessons { get; set; }

    public decimal EvaluatorType { get; set; }

    public decimal TrainerType { get; set; }

    public decimal SameTrainerEvaluator { get; set; }

    public decimal SupervisorEvalException { get; set; }

    public virtual LsEvaluatorTrainerValue EvaluatorTypeNavigation { get; set; }

    public virtual LsCatalogLesson FkCatalogLessonsNavigation { get; set; }

    public virtual LsEvaluatorTrainerValue TrainerTypeNavigation { get; set; }
}
