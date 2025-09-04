using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsProgramCompletion
{
    public decimal Id { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkLearningEvent { get; set; }

    public decimal? FkQualEvent { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateCompleted { get; set; }

    public decimal Status { get; set; }

    public DateTime? DateExpires { get; set; }

    public decimal? Exempt { get; set; }

    public string ExemptComments { get; set; }

    public decimal Archive { get; set; }

    public string Comments { get; set; }

    public decimal? FkCatalog { get; set; }

    public virtual LsCatalog FkCatalogNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual LsQualCardEvent FkQualEventNavigation { get; set; }

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();
}
