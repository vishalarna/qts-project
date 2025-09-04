using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjtreportSelectedSetting
    {
        public int SettingsId { get; set; }
        public string Title { get; set; }
        public string SettingsDesc { get; set; }
        public DateTime Dra { get; set; }
        public string Raby { get; set; }
    }
}
