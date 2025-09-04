using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_TrainingGroupsFromRstblPositionsTasksMap : Common.MigrationMap<RstblPositionsTask, Task_TrainingGroup>
    {

        List<RstblPositionsTask> _positionsTasks;
        public Task_TrainingGroupsFromRstblPositionsTasksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RstblPositionsTask> getSourceRecords()
        {
            _positionsTasks = (_source as EMP_DemoContext).RstblPositionsTasks.ToListAsync().Result;
            return _positionsTasks;
        }
        protected override Task_TrainingGroup mapRecord(RstblPositionsTask obj)
        {
            return new Task_TrainingGroup()
            {
                TaskId=obj.Tid,
                //TrainingGroupId
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true,
            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _positionsTasks.Count();
        }
        protected override void updateTarget(Task_TrainingGroup record)
        {
            (_target as QTD2.Data.QTDContext).Task_TrainingGroups.Add(record);
        }

    }
}
