using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Task_ILA_LinksMap : Common.MigrationMap<RsTblCoursesTask, Task_ILA_Link>
    {
        List<RsTblCoursesTask> _tblCoursesTask;

        public Task_ILA_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblCoursesTask> getSourceRecords()
        {
            _tblCoursesTask = (_source as EMP_DemoContext).RsTblCoursesTasks.ToListAsync().Result;
            return _tblCoursesTask;
        }

        protected override Task_ILA_Link mapRecord(RsTblCoursesTask obj)
        {
            return new Task_ILA_Link()
            {
                Active = true,
                TaskId=obj.Tid,
                //ILAId
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tblCoursesTask.Count();
        }

        protected override void updateTarget(Task_ILA_Link record)
        {
            (_target as QTD2.Data.QTDContext).Task_ILA_Links.Add(record);
        }
    }
}
