using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCourseType
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsSurvey> LsSurveys { get; set; } = new List<LsSurvey>();
}
