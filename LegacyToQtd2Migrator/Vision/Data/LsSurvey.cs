using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurvey
{
    public decimal Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Usercode { get; set; }

    public decimal FkCourseType { get; set; }

    public decimal? FkCompany { get; set; }

    public decimal? FkJob { get; set; }

    public decimal FkDifrangescale { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal FkChangedBy { get; set; }

    public DateTime ChangedDate { get; set; }

    public decimal Isactive { get; set; }

    public virtual Learner FkChangedByNavigation { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual LsCourseType FkCourseTypeNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual LsSurveyDifRatingscale FkDifrangescaleNavigation { get; set; }

    public virtual LsOrg FkJobNavigation { get; set; }

    public virtual ICollection<LsSurveyEvent> LsSurveyEvents { get; set; } = new List<LsSurveyEvent>();

    public virtual ICollection<LsSurveyItem> LsSurveyItems { get; set; } = new List<LsSurveyItem>();
}
