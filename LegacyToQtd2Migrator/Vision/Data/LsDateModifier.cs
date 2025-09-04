using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsDateModifier
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public decimal? FkTimeType { get; set; }

    public decimal? TimeSpan { get; set; }

    public virtual TimeType FkTimeTypeNavigation { get; set; }

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsImportJobCourse> LsImportJobCourses { get; set; } = new List<LsImportJobCourse>();
}
