using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_TaskObjective_LinksMap : Common.MigrationMap<RsTblCoursesTask, ILA_TaskObjective_Link>
    {
        List<RsTblCoursesTask> _coursetask;
        public ILA_TaskObjective_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<RsTblCoursesTask> getSourceRecords()
        {
            _coursetask = (_source as EMP_DemoContext).RsTblCoursesTasks.ToList();

            return _coursetask;
        }

        protected override ILA_TaskObjective_Link mapRecord(RsTblCoursesTask obj)
        {
            var sourceIla = obj.Cor;
            var sourceTask = obj.TidNavigation;
            var sourceDutyArea = obj.TidNavigation.Da;

            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).Where(r => r.Description == sourceIla.Cordesc).First();
            var targetTask = (_target as QTD2.Data.QTDContext)
                .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                .Tasks.Where(r => r.Number == sourceTask.Tnum).First();


            return new ILA_TaskObjective_Link()
            {
                ILAId = targetIla.Id,
                TaskId = targetTask.Id,
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _coursetask.Count();
        }

        protected override void updateTarget(ILA_TaskObjective_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_TaskObjective_Links.Add(record);
        }
    }
}
