using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SimulatorScenarioEnablingObjectivesLinksMap : Common.MigrationMap<TblIlaSimulationObjective, SimulatorScenario_EnablingObjectives_Link_Old>
    {
        List<TblIlaSimulationObjective> _simulatorScenarioObjective;

        public SimulatorScenarioEnablingObjectivesLinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblIlaSimulationObjective> getSourceRecords()
        {
            _simulatorScenarioObjective = (_source as EMP_DemoContext).TblIlaSimulationObjectives.ToListAsync().Result;
            return _simulatorScenarioObjective;
        }

        protected override SimulatorScenario_EnablingObjectives_Link_Old mapRecord(TblIlaSimulationObjective obj)
        {
            return new SimulatorScenario_EnablingObjectives_Link_Old()
            {
                Active = true,
                EnablingObjectiveID=obj.ObjectiveId,
                //SimulatorScenarioID
     
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _simulatorScenarioObjective.Count();
        }

        protected override void updateTarget(SimulatorScenario_EnablingObjectives_Link_Old record)
        {
            (_target as QTD2.Data.QTDContext).SimulatorScenario_EnablingObjectives_Links_Old.Add(record);
        }
    }
}
