using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyeventItem
{
    public decimal Id { get; set; }

    public decimal FkSeventid { get; set; }

    public string UserdefinedId { get; set; }

    public decimal FkObjectId { get; set; }

    public decimal FkSurveyObjType { get; set; }

    public decimal Sequence { get; set; }

    public decimal FkLearnerChangedBy { get; set; }

    public DateTime ChangedDate { get; set; }

    public decimal Isactive { get; set; }

    public virtual Learner FkLearnerChangedByNavigation { get; set; }

    public virtual LsSurveyEvent FkSevent { get; set; }

    public virtual LsSurveyObjectType FkSurveyObjTypeNavigation { get; set; }

    public virtual ICollection<LsSurveyItemResultsDif> LsSurveyItemResultsDifs { get; set; } = new List<LsSurveyItemResultsDif>();
}
