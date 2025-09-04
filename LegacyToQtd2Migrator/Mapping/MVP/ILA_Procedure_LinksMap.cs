using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_Procedure_LinksMap :Common.MigrationMap<TblCourse, ILA_Procedure_Link>
    {
        List<TblCourse> _course;
        public ILA_Procedure_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            return _course;
        }

        protected override ILA_Procedure_Link mapRecord(TblCourse obj)
        {
          //  var sourceProc = (_source as EMP_DemoContext)

           // var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();
           // var targetProc = (_target as QTD2.Data.QTDContext).Procedures.Where(r => r.Title == sourceProc.Shztitle).First();

            return new ILA_Procedure_Link()
            {
                //ILAId
                //ProcedureId
                //CreatedBy
                //CreatedDate,
                //ModifiedBy,
                //ModifiedDate
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(ILA_Procedure_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_Procedure_Links.Add(record);
        }
    }
}
