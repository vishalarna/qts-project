using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class PensCommand
    {
        public PensCommand()
        {
            PensCommandScormPackages = new HashSet<PensCommandScormPackage>();
        }

        public int PensCommandId { get; set; }
        public string PensPackageType { get; set; }
        public string PensPackageId { get; set; }
        public string PensClient { get; set; }
        public string PensSystemUserId { get; set; }
        public string PensStep { get; set; }
        public int InternalStep { get; set; }
        public bool ShouldProcess { get; set; }
        public string PensCommand1 { get; set; }
        public string PensSerialized { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public int FailedCount { get; set; }
        public DateTime ProcessAfter { get; set; }
        public string LockId { get; set; }

        public virtual ICollection<PensCommandScormPackage> PensCommandScormPackages { get; set; }
    }
}
