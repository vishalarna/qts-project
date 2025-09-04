using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsImportJobAssignment
{
    public decimal Id { get; set; }

    public string Pin { get; set; }

    public string JobCode { get; set; }

    public string Action { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal Xfer { get; set; }

    public DateTime? DateXfer { get; set; }

    public string XferMessage { get; set; }
}
