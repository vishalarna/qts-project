using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class ProcedureReviewMap : Common.MigrationMap<TblCourse, ProcedureReview>
    {
        List<TblCourse> _course;

        public ProcedureReviewMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.Where(r => !string.IsNullOrEmpty(r.CourseProcedures)).ToListAsync().Result;
            return _course;
        }

        protected override ProcedureReview mapRecord(TblCourse obj)
        {
            throw new System.NotImplementedException();
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(ProcedureReview record)
        {
            (_target as QTD2.Data.QTDContext).ProcedureReviews.Add(record);
        }
    }
}
