using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaSimulationScript
    {
        public int ScriptId { get; set; }
        public int ScriptSequence { get; set; }
        public int IlasimId { get; set; }
        public string Initiator { get; set; }
        public string Event { get; set; }
        public int? Ilascid { get; set; }
        public int? InitiatorPosId { get; set; }

        public virtual TblIlaSimulationCriterion Ilasc { get; set; }
        public virtual TblIlaSimulation Ilasim { get; set; }
    }
}
