using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCopresentationCo
    {
        public int Pid { get; set; }
        public int Coid { get; set; }
        public int Sequence { get; set; }
        public int? Skid { get; set; }

        public virtual TblContentObject Co { get; set; }
        public virtual TblContentObjectPresentation PidNavigation { get; set; }
    }
}
