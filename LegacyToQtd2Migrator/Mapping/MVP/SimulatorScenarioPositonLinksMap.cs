using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SimulatorScenarioPositonLinksMap : Common.MigrationMap<TblIlaSimulationPosition, SimulatorScenarioPositon_Link_Old>
    {
        List<TblIlaSimulationPosition> _ilaSimulationPosition;

        public SimulatorScenarioPositonLinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblIlaSimulationPosition> getSourceRecords()
        {
            _ilaSimulationPosition = (_source as EMP_DemoContext).TblIlaSimulationPositions.ToListAsync().Result;
            return _ilaSimulationPosition;
        }

        protected override SimulatorScenarioPositon_Link_Old mapRecord(TblIlaSimulationPosition obj)
        {
            return new SimulatorScenarioPositon_Link_Old()
            {
                Active = true,
                PositionID=obj.PosId,
                //SimulatorScenarioID

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilaSimulationPosition.Count();
        }

        protected override void updateTarget(SimulatorScenarioPositon_Link_Old record)
        {
            (_target as QTD2.Data.QTDContext).SimulatorScenarioPositon_Links_Old.Add(record);
        }
    }
}
