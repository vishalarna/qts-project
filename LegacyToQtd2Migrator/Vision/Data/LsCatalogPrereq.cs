using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCatalogPrereq
{
    public decimal Id { get; set; }

    public decimal FkCatalog { get; set; }

    public decimal FkPrerequisite { get; set; }

    public string Text { get; set; }

    public decimal Type { get; set; }

    public decimal Status { get; set; }

    public decimal? Sequence { get; set; }

    public virtual LsCatalog FkCatalogNavigation { get; set; }
}
