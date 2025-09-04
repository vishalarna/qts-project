using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyEvent
{
    public decimal Id { get; set; }

    public string Title { get; set; }

    public decimal FkSurveys { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? AdminEndDate { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal? FkChangedBy { get; set; }

    public DateTime ChangedDate { get; set; }

    public decimal Isactive { get; set; }

    public virtual Learner FkChangedByNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual LsSurvey FkSurveysNavigation { get; set; }

    public virtual ICollection<LsSurveyItemResultsDif> LsSurveyItemResultsDifs { get; set; } = new List<LsSurveyItemResultsDif>();

    public virtual ICollection<LsSurveyeventItem> LsSurveyeventItems { get; set; } = new List<LsSurveyeventItem>();

    public virtual ICollection<LsSurveyeventRespondent> LsSurveyeventRespondents { get; set; } = new List<LsSurveyeventRespondent>();
}
