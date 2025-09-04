using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_ReferencesMap : Common.MigrationMap<TblTask, List<Task_Reference>>
    {
        List<TblTask> _taskRefrences;
        public Task_ReferencesMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblTask> getSourceRecords()
        {
            _taskRefrences = (_source as EMP_DemoContext).TblTasks.Where(r => r.Treferences != null).ToListAsync().Result;
            return _taskRefrences;
        }

        protected override List<Task_Reference> mapRecord(TblTask obj)
        {
            List<Task_Reference> references = new List<Task_Reference>();

            var objs = obj.Treferences.Split(" ");

            foreach (var o in objs)
            {
                references.Add(new Task_Reference()
                {
                    Active = true,
                    DisplayName = o,
                    Description = o,
                    Deleted = false
                });
            }

            return references;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _taskRefrences.Count();
        }

        protected override void updateTarget(List<Task_Reference> record)
        {
            (_target as QTD2.Data.QTDContext).Task_References.AddRange(record);
        }

    }
}
