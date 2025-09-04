using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCardEvent
{
    public decimal Id { get; set; }

    public decimal FkLsQualCard { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkQualEventScore { get; set; }

    public DateTime? DateOpen { get; set; }

    public DateTime? DateClose { get; set; }

    public DateTime? DateToBeCompleteBy { get; set; }

    public DateTime? DateExpires { get; set; }

    public string Comments { get; set; }

    public decimal FkTrainingEventScore { get; set; }

    public DateTime? CompletedTrainingDate { get; set; }

    public string AiccSessionid { get; set; }

    public DateTime? OriginalDateClose { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual LsStatus FkQualEventScoreNavigation { get; set; }

    public virtual LsStatus FkTrainingEventScoreNavigation { get; set; }

    public virtual ICollection<LsProgramCompletion> LsProgramCompletions { get; set; } = new List<LsProgramCompletion>();
}
