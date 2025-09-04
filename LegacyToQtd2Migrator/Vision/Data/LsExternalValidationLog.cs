using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExternalValidationLog
{
    public DateTime DateCreated { get; set; }

    public decimal? RecCount { get; set; }

    public string OperationComment { get; set; }
}
