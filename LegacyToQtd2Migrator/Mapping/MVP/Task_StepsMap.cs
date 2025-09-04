using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_StepsMap : Common.MigrationMap<TblTask, Task_Step>
    {
        List<TblTask> _taskSteps;
        public Task_StepsMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblTask> getSourceRecords()
        {
            _taskSteps = (_source as EMP_DemoContext).TblTasks.ToListAsync().Result;
            return _taskSteps;
        }

        protected override Task_Step mapRecord(TblTask obj)
        {
            return new Task_Step()
            {
                Active = true,
                TaskId=obj.Tid,
                Description=obj.Tdesc,
                Number=obj.Tnum,
                //ParentStepId,
                Deleted=false

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _taskSteps.Count();
        }

        protected override void updateTarget(Task_Step record)
        {
            (_target as QTD2.Data.QTDContext).Task_Steps.Add(record);
        }

    }
}
