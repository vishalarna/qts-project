using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyDifRatingscale
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public decimal Isactive { get; set; }

    public virtual ICollection<LsSurvey> LsSurveys { get; set; } = new List<LsSurvey>();
}
