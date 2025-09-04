using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_ToolsFromTblTasksMap : Common.MigrationMap<TblTask, Task_Tool>
    {
        List<TblTask> _tasks;

        public Task_ToolsFromTblTasksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTask> getSourceRecords()
        {
            _tasks = (_source as EMP_DemoContext).TblTasks.ToListAsync().Result;
            return _tasks;
        }

        protected override Task_Tool mapRecord(TblTask obj)
        {
            return new Task_Tool()
            {
                TaskId=obj.Tid,
                //ToolId
                Deleted = false,
                Active = true,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tasks.Count();
        }

        protected override void updateTarget(Task_Tool record)
        {
            (_target as QTD2.Data.QTDContext).Task_Tools.Add(record);
        }
    }
}
