using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class PensCommandScormPackage
    {
        public int PensCommandId { get; set; }
        public int ScormPackageId { get; set; }
        public string ExternalPackageId { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }

        public virtual PensCommand PensCommand { get; set; }
    }
}
