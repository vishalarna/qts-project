using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RstblPositionsTask
    {
        public RstblPositionsTask()
        {
            TblTaskLinkages = new HashSet<TblTaskLinkage>();
        }

        public int Pid { get; set; }
        public int Tid { get; set; }
        public double? AvgDifficulty { get; set; }
        public double? AvgImportance { get; set; }
        public double? AvgFrequency { get; set; }
        public int? StatusDefault { get; set; }
        public int? StatusFinal { get; set; }
        public bool Critical { get; set; }
        public int? Tpid { get; set; }
        public byte[] Ts { get; set; }
        public string DifComments { get; set; }
        public string RrReason { get; set; }
        public bool? ImpactR6 { get; set; }
        public string R6Reason { get; set; }

        public virtual TblPosition PidNavigation { get; set; }
        public virtual TblTask TidNavigation { get; set; }
        public virtual ICollection<TblTaskLinkage> TblTaskLinkages { get; set; }
    }
}
