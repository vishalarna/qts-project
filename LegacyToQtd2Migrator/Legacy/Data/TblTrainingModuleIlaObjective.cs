using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModuleIlaObjective
    {
        public int Tmid { get; set; }
        public int Corid { get; set; }
        public int ObId { get; set; }
        public string ObjType { get; set; }

        public virtual TblTrainingModuleIla TblTrainingModuleIla { get; set; }
    }
}
