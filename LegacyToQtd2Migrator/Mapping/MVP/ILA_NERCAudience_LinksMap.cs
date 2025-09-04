using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_NERCAudience_LinksMap :Common.MigrationMap<TblCourse, ILA_NERCAudience_Link>
    {
        List<TblCourse> _course;
        public ILA_NERCAudience_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourse> getSourceRecords()
        {
            _course = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            return _course;
        }

        protected override ILA_NERCAudience_Link mapRecord(TblCourse obj)
        {
            return new ILA_NERCAudience_Link()
            { 
                //ILAId
                //NERCAudienceID
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
            TotalRecordsToConvert = _course.Count();
        }

        protected override void updateTarget(ILA_NERCAudience_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_NERCAudience_Links.Add(record);
        }
    }
}
