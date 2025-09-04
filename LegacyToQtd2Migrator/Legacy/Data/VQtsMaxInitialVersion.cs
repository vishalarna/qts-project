using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsMaxInitialVersion
    {
        public int Pid { get; set; }
        public decimal? MaxVersion { get; set; }
        public DateTime? Tpdate { get; set; }
        public int Ptpid { get; set; }
    }
}
