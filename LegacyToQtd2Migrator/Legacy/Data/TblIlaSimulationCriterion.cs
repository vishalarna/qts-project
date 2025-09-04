using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaSimulationCriterion
    {
        public TblIlaSimulationCriterion()
        {
            TblIlaSimulationScripts = new HashSet<TblIlaSimulationScript>();
        }

        public int Ilascid { get; set; }
        public int IlasimId { get; set; }
        public int Sequence { get; set; }
        public int PosId { get; set; }
        public string CriteriaDesc { get; set; }

        public virtual ICollection<TblIlaSimulationScript> TblIlaSimulationScripts { get; set; }
    }
}
