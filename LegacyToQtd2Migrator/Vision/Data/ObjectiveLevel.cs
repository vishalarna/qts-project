using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveLevel
{
    public decimal Id { get; set; }

    public virtual ICollection<ExamProfileByObjective> ExamProfileByObjectives { get; set; } = new List<ExamProfileByObjective>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveLevelImpl> ObjectiveLevelImpls { get; set; } = new List<ObjectiveLevelImpl>();
}
