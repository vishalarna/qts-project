using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class OfficeMigration
{
    public decimal FkContent { get; set; }

    public virtual Content FkContentNavigation { get; set; }
}
