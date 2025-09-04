using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_PositionsMap : Common.MigrationMap<RstblPositionsTask, Task_Position>
    {
        List<RstblPositionsTask> _tblPositionsTask;

        public Task_PositionsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RstblPositionsTask> getSourceRecords()
        {
            _tblPositionsTask = (_source as EMP_DemoContext).RstblPositionsTasks.ToListAsync().Result;
            return _tblPositionsTask;
        }

        protected override Task_Position mapRecord(RstblPositionsTask obj)
        {
            return new Task_Position()
            {
                Active = true,
                TaskId=obj.Tid,
                PositionId=obj.Pid,

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblPositionsTask.Count();
        }

        protected override void updateTarget(Task_Position record)
        {
            (_target as QTD2.Data.QTDContext).Task_Positions.Add(record);
        }
    }
}
