using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModuleTask
    {
        public int Tmid { get; set; }
        public int Tid { get; set; }

        public virtual TblTask TidNavigation { get; set; }
        public virtual TblTrainingModule Tm { get; set; }
    }
}
