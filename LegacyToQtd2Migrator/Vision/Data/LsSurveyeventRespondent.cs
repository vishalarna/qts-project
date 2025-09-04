using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyeventRespondent
{
    public decimal Id { get; set; }

    public decimal FkSeventid { get; set; }

    public decimal FkLearner { get; set; }

    public string OverallEvntComments { get; set; }

    public decimal? FkChangedBy { get; set; }

    public DateTime ChangedDate { get; set; }

    public decimal Eventcompleted { get; set; }

    public decimal Isactive { get; set; }

    public virtual Learner FkChangedByNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsSurveyEvent FkSevent { get; set; }

    public virtual ICollection<LsSurveyItemResultsDif> LsSurveyItemResultsDifs { get; set; } = new List<LsSurveyItemResultsDif>();
}
