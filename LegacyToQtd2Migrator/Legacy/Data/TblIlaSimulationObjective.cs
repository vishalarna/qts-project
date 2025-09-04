using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaSimulationObjective
    {
        public int Ilasimid { get; set; }
        public int Corid { get; set; }
        public int ObjectiveId { get; set; }
        public string ObjectiveType { get; set; }
        public int? PosId { get; set; }
        public int? SktaskId { get; set; }
        public int? Sequence { get; set; }
    }
}
