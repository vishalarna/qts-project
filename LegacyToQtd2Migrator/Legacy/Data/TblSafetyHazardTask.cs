using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardTask
    {
        public int Shztid { get; set; }
        public int Tid { get; set; }
        public int Shzid { get; set; }

        public virtual TblSafetyHazard Shz { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
