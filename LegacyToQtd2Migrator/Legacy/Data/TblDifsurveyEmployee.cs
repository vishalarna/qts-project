using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblDifsurveyEmployee
    {
        public int Difid { get; set; }
        public int Difprjid { get; set; }
        public int Eid { get; set; }
        public int Tid { get; set; }
        public double? Difficulty { get; set; }
        public double? Importance { get; set; }
        public double? Frequency { get; set; }
        public bool? Na { get; set; }
        public string Comments { get; set; }
        public int Pid { get; set; }

        public virtual TblDifproject Difprj { get; set; }
        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblPosition PidNavigation { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
