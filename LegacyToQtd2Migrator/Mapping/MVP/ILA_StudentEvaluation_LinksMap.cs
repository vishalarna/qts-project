using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class ILA_StudentEvaluation_LinksMap : Common.MigrationMap<TblCourse, ILA_StudentEvaluation_Link>
    {
        List<TblCourse> _course;
        public ILA_StudentEvaluation_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.Where(r => r.Fid.HasValue).ToListAsync().Result;
            return _course;
        }

        protected override ILA_StudentEvaluation_Link mapRecord(TblCourse obj)
        {
            var sourceForm = (_source as EMP_DemoContext).TblForms.Where(r => r.Fid == obj.Fid).First();

            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == obj.Cornum).First();
            var targetStudentEvaluation = (_target as QTD2.Data.QTDContext).StudentEvaluations.Where(r => r.Title == sourceForm.Fname).First();

            return new ILA_StudentEvaluation_Link()
            {
                ILAId = targetIla.Id,
                studentEvalFormID = targetStudentEvaluation.Id,
                //studentEvalAvailabilityID
                //studentEvalAudienceID
                //isAllQuestionMandatory
                //StudentEvaluationAvailabilityId
                //StudentEvaluationAudienceId
                //StudentEvaluationFormId
                //CreatedBy
                //CreatedDate,
                //ModifiedBy,
                //ModifiedDate
                Deleted = false,
                Active=true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(ILA_StudentEvaluation_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_StudentEvaluation_Links.Add(record);
        }
    }
}
