using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_HistoriesFromTblTasksHistoryMap : Common.MigrationMap<TblTasksHistory, Task_History>
    {
        List<TblTasksHistory> _tasksHistories;
        public Task_HistoriesFromTblTasksHistoryMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTasksHistory> getSourceRecords()
        {
            _tasksHistories = (_source as EMP_DemoContext).TblTasksHistories.ToListAsync().Result;
            return _tasksHistories;
        }
        protected override Task_History mapRecord(TblTasksHistory obj)
        {
            return new Task_History()
            {
                TaskId =obj.Tid??-1,
                //Version_TaskId
                //OldStatus 
                //NewStatus
                //ChangeEffectiveDate,
                ChangeNotes = obj.Thnote,
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
            TotalRecordsToConvert = _tasksHistories.Count();
        }
        protected override void updateTarget(Task_History record)
        {
            (_target as QTD2.Data.QTDContext).Task_Histories.Add(record);
        }
    }
}
