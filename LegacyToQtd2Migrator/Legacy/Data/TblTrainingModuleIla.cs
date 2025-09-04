using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModuleIla
    {
        public TblTrainingModuleIla()
        {
            TblTrainingModuleIlaObjectives = new HashSet<TblTrainingModuleIlaObjective>();
        }

        public int Tmid { get; set; }
        public int Corid { get; set; }
        public int? SortOrder { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblTrainingModule Tm { get; set; }
        public virtual ICollection<TblTrainingModuleIlaObjective> TblTrainingModuleIlaObjectives { get; set; }
    }
}
