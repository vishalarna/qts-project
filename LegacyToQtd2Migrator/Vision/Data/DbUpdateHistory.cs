using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class DbUpdateHistory
{
    public string ScriptName { get; set; }

    public string ScriptVersion { get; set; }

    public DateTime DateUpdated { get; set; }
}
