using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class SimulatorScenarioILALinksMap : Common.MigrationMap<TblIlaSimulation, SimulatorScenarioILA_Link_Old>
    {
        List<TblIlaSimulation> _ilaSimulation;

        public SimulatorScenarioILALinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblIlaSimulation> getSourceRecords()
        {
            _ilaSimulation = (_source as EMP_DemoContext).TblIlaSimulations.ToListAsync().Result;
            return _ilaSimulation;
        }

        protected override SimulatorScenarioILA_Link_Old mapRecord(TblIlaSimulation obj)
        {
            return new SimulatorScenarioILA_Link_Old()
            {
                Active = true,
                ILAID=obj.IlasimId,
                //SimulatorScenarioID

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilaSimulation.Count();
        }

        protected override void updateTarget(SimulatorScenarioILA_Link_Old record)
        {
            (_target as QTD2.Data.QTDContext).SimulatorScenarioILA_Links_Old.Add(record);
        }
    }
}
