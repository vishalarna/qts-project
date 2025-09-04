using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTuItemType
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public decimal Type { get; set; }

    public virtual ICollection<LsPaEvaluatorTrainer> LsPaEvaluatorTrainers { get; set; } = new List<LsPaEvaluatorTrainer>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequests { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsTaskQualification> LsTaskQualifications { get; set; } = new List<LsTaskQualification>();
}
