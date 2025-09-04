using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SimulationScenarioSpecLookUpsMap : Common.MigrationMap<TblIlaSimulationScript, SimulationScenarioSpecLookUp_Old>
    {
        List<TblIlaSimulationScript> _simulationScenarioSpecsLookUps;
        public SimulationScenarioSpecLookUpsMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblIlaSimulationScript> getSourceRecords()
        {
            _simulationScenarioSpecsLookUps = (_source as EMP_DemoContext).TblIlaSimulationScripts.ToListAsync().Result;
            return _simulationScenarioSpecsLookUps;
        }

        protected override SimulationScenarioSpecLookUp_Old mapRecord(TblIlaSimulationScript obj)
        {
            return new SimulationScenarioSpecLookUp_Old()
            {
                Active = true,
                //SimScenarioSpecHeading
               

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _simulationScenarioSpecsLookUps.Count();
        }

        protected override void updateTarget(SimulationScenarioSpecLookUp_Old record)
        {
            (_target as QTD2.Data.QTDContext).SimulationScenarioSpecLookUps_Old.Add(record);
        }

    }
}
