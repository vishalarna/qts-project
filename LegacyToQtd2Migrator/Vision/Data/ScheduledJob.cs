using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ScheduledJob
{
    public string Name { get; set; }

    public DateTime DateLastRun { get; set; }
}
