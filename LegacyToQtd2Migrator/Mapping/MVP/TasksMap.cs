using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TasksMap : Common.MigrationMap<TblTask, Task>
    {
        List<TblTask> _tblTask;

        public TasksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTask> getSourceRecords()
        {
            _tblTask = (_source as EMP_DemoContext).TblTasks.ToListAsync().Result;
            return _tblTask;
        }

        protected override Task mapRecord(TblTask obj)
        {
            return new Task()
            {
                Active = true,
                //SubdutyAreaId,
                Description=obj.Tdesc,
                Abreviation=obj.Tabbrev,
                Number=obj.Tnum??=-1,
                //Criteria,
                //TaskCriteriaUpload
                //RequalificationDueDate,
                //RequalificationRequired,
                //RequalificationNotes
                //Image
                Critical=obj.Critical,
                References=obj.Treferences,
                //RequiredTime,
                //IsMeta,
                //IsReliability
                Conditions=obj.Tconditions,
                //EffectiveDate
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblTask.Count();
        }

        protected override void updateTarget(Task record)
        {
            (_target as QTD2.Data.QTDContext).Tasks.Add(record);
        }
    }
}
