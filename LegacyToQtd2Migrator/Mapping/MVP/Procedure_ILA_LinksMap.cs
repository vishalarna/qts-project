using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Procedure_ILA_LinksMap : Common.MigrationMap<TblCourse, List<Procedure_ILA_Link>>
    {
        List<TblCourse> _course;
        public Procedure_ILA_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.Where(r => !string.IsNullOrEmpty(r.CourseProcedures)).ToListAsync().Result;
            return _course;
        }

        protected override List<Procedure_ILA_Link> mapRecord(TblCourse obj)
        {
            List<Procedure_ILA_Link> links = new List<Procedure_ILA_Link>();
            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == obj.Cornum).First();

            var courseProcedures = obj.CourseProcedures.Split(System.Environment.NewLine);

            foreach (var courseProcedure in courseProcedures)
            {
                var targetProcedure = (_target as QTD2.Data.QTDContext).Procedures.Where(r => r.Title == courseProcedure).First();

                links.Add(new Procedure_ILA_Link()
                {
                    Active = true,
                    ProcedureId = targetProcedure.Id,
                    ILAId = targetIla.Id
                });
            }

            return links;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(List<Procedure_ILA_Link> records)
        {
            (_target as QTD2.Data.QTDContext).Procedure_ILA_Links.AddRange(records);
        }
    }
}
