using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ClassSchedule_StudentEvaluations_LinksMap : Common.MigrationMap<TblCourse, ClassSchedule_StudentEvaluations_Link>
    {
        List<TblCourse> _courses;
        List<TblForm> _forms;
        public ClassSchedule_StudentEvaluations_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }
        protected override List<TblCourse> getSourceRecords()
        {
            _courses = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            _forms = (_source as EMP_DemoContext).TblForms.ToListAsync().Result;
            return _courses;
        }

        protected override ClassSchedule_StudentEvaluations_Link mapRecord(TblCourse obj)
        {
            return new ClassSchedule_StudentEvaluations_Link()
            {
                //StudentEvaluationId
                //ClassScheduleId
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
            TotalRecordsToConvert = _courses.Count();
        }

        protected override void updateTarget(ClassSchedule_StudentEvaluations_Link record)
        {
            (_target as QTD2.Data.QTDContext).ClassSchedule_StudentEvaluations_Links.Add(record);
        }
    }
}
