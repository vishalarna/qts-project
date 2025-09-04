using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class Task_HistoriesFromTblPositionTasksHistoryMap : Common.MigrationMap<TblPositionTasksHistory, Task_History>
    {
        List<TblPositionTasksHistory> _positionTasksHistories;
        public Task_HistoriesFromTblPositionTasksHistoryMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblPositionTasksHistory> getSourceRecords()
        {
            _positionTasksHistories = (_source as EMP_DemoContext).TblPositionTasksHistories.ToListAsync().Result;
            return _positionTasksHistories;
        }
        protected override Task_History mapRecord(TblPositionTasksHistory obj)
        {
            return new Task_History()
            {
                TaskId = obj.Tid ?? -1,
                //Version_TaskId
                //OldStatus 
                //NewStatus
                //ChangeEffectiveDate,
                ChangeNotes = obj.Pthnote,
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
            TotalRecordsToConvert = _positionTasksHistories.Count();
        }
        protected override void updateTarget(Task_History record)
        {
            (_target as QTD2.Data.QTDContext).Task_Histories.Add(record);
        }
    }
}
