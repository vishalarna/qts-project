using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyItemResultsDif
{
    public decimal Id { get; set; }

    public decimal FkSeventid { get; set; }

    public decimal FkSurvyitemId { get; set; }

    public decimal FkSevntItemid { get; set; }

    public decimal FkSevntRespondent { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? ValueDiff { get; set; }

    public decimal? ValueImp { get; set; }

    public decimal? ValueFreq { get; set; }

    public string ItemComments { get; set; }

    public decimal FkLearnerChangedby { get; set; }

    public DateTime ChangedDate { get; set; }

    public decimal Isactive { get; set; }

    public virtual Learner FkLearnerChangedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsSurveyEvent FkSevent { get; set; }

    public virtual LsSurveyeventItem FkSevntItem { get; set; }

    public virtual LsSurveyeventRespondent FkSevntRespondentNavigation { get; set; }

    public virtual LsSurveyItem FkSurvyitem { get; set; }
}
