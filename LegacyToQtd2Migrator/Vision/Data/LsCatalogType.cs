using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCatalogType
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public decimal Status { get; set; }

    public virtual ICollection<LsCatalogLesson> LsCatalogLessons { get; set; } = new List<LsCatalogLesson>();

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();
}
