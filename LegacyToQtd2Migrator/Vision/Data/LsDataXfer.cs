using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsDataXfer
{
    public decimal Id { get; set; }

    public string Ssn { get; set; }

    public string TrainingType { get; set; }

    public DateTime? ClassDate { get; set; }

    public string TestMethod { get; set; }

    public decimal? PassFail { get; set; }

    public DateTime? ClassTime { get; set; }

    public decimal RecUpdated { get; set; }
}
