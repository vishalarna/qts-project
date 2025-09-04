using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGapRating
    {
        public int Gapid { get; set; }
        public int Prjid { get; set; }
        public int Eid { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public float? Rating { get; set; }
        public string Comments { get; set; }
        public DateTime? Dra { get; set; }
        public string Raby { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblPosition PidNavigation { get; set; }
        public virtual TblGapProject Prj { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
