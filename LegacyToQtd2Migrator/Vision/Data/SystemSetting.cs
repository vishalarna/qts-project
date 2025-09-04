using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class SystemSetting
{
    public decimal? PasswordWarnDays { get; set; }

    public string PathReports { get; set; }

    public string PathOfficeReports { get; set; }

    public string PathTestFilters { get; set; }

    public string PathContentTemplates { get; set; }

    public string PathGraphics { get; set; }

    public string PathSpellCheck { get; set; }

    public string PathLicense { get; set; }

    public string PathProjectTemplates { get; set; }
}
