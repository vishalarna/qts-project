using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Import
{
    public decimal DataType { get; set; }

    public decimal FkData { get; set; }

    public decimal FkDataProject { get; set; }

    public string ImportId { get; set; }

    public decimal FkDataParent { get; set; }

    public DateTime? RevisionDate { get; set; }

    public virtual Project FkDataProjectNavigation { get; set; }
}
