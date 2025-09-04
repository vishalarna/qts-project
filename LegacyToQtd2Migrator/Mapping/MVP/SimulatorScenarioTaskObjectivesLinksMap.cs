using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SimulatorScenarioTaskObjectivesLinksMap : Common.MigrationMap<TblIlaSimulationObjective, SimulatorScenarioTaskObjectives_Link_Old>
    {
        List<TblIlaSimulationObjective> _ilaSimulationObjective;

        public SimulatorScenarioTaskObjectivesLinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblIlaSimulationObjective> getSourceRecords()
        {
            _ilaSimulationObjective = (_source as EMP_DemoContext).TblIlaSimulationObjectives.ToListAsync().Result;
            return _ilaSimulationObjective;
        }

        protected override SimulatorScenarioTaskObjectives_Link_Old mapRecord(TblIlaSimulationObjective obj)
        {
            return new SimulatorScenarioTaskObjectives_Link_Old()
            {
                Active = true,
                TaskID=obj.SktaskId??=-1,
                //SimulatorScenarioID
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilaSimulationObjective.Count();
        }

        protected override void updateTarget(SimulatorScenarioTaskObjectives_Link_Old record)
        {
            (_target as QTD2.Data.QTDContext).SimulatorScenarioTaskObjectives_Links_Old.Add(record);
        }
    }
}
