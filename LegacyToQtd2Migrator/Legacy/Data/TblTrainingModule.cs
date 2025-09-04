using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModule
    {
        public TblTrainingModule()
        {
            TblTrainingModuleIlas = new HashSet<TblTrainingModuleIla>();
            TblTrainingModuleSks = new HashSet<TblTrainingModuleSk>();
            TblTrainingModuleTasks = new HashSet<TblTrainingModuleTask>();
        }

        public int Tmid { get; set; }
        public string Tmnumber { get; set; }
        public string Tmname { get; set; }
        public string Tmdesc { get; set; }
        public string Tmprereq { get; set; }
        public bool? Tmactive { get; set; }
        public string Tmprocedure { get; set; }
        public string Tmresource { get; set; }

        public virtual ICollection<TblTrainingModuleIla> TblTrainingModuleIlas { get; set; }
        public virtual ICollection<TblTrainingModuleSk> TblTrainingModuleSks { get; set; }
        public virtual ICollection<TblTrainingModuleTask> TblTrainingModuleTasks { get; set; }
    }
}
