using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class AppVersion
{
    public string Application { get; set; }

    public string Version { get; set; }

    public DateTime DateModified { get; set; }

    public decimal IsInstalled { get; set; }

    public string LicenseKey { get; set; }

    public decimal PwdEncryption { get; set; }
}
