using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaSimulation
    {
        public TblIlaSimulation()
        {
            TblIlaSimulationScripts = new HashSet<TblIlaSimulationScript>();
        }

        public int IlasimId { get; set; }
        public int? Corid { get; set; }
        public string NetworkConfiguration { get; set; }
        public string LoadingConditions { get; set; }
        public string Generation { get; set; }
        public string Interchange { get; set; }
        public string Other { get; set; }
        public string ValidityChecks { get; set; }
        public string RolePlays { get; set; }
        public string DocumentationProcedures { get; set; }
        public string Notes { get; set; }
        public string ScenarioDesc { get; set; }
        public string InstructorPrep { get; set; }
        public string ScenarioTitle { get; set; }

        public virtual ICollection<TblIlaSimulationScript> TblIlaSimulationScripts { get; set; }
    }
}
