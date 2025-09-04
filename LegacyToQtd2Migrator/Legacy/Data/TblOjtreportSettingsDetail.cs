using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjtreportSettingsDetail
    {
        public int SettingsId { get; set; }
        public string ControlName { get; set; }
        public string Value { get; set; }

        public virtual TblOjtreportSelectedSetting Settings { get; set; }
    }
}
