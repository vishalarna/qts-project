using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class ILA_Segment_LinksMap :Common.MigrationMap<TblCourseSegment, ILA_Segment_Link>
    {
        List<TblCourseSegment> _coursesegment;
        public ILA_Segment_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourseSegment> getSourceRecords()
        {
            _coursesegment = (_source as EMP_DemoContext).TblCourseSegments.ToListAsync().Result;
            return _coursesegment;
        }

        protected override ILA_Segment_Link mapRecord(TblCourseSegment obj)
        {
            var sourceIla = obj.Cor;
            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();

            return new ILA_Segment_Link()
            {
                ILAId = targetIla.Id,
                //SegmentId
                //CreatedBy
                //CreatedDate,
                //ModifiedBy,
                //ModifiedDate
                Deleted = false,
                Active = true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _coursesegment.Count();
        }

        protected override void updateTarget(ILA_Segment_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_Segment_Links.Add(record);
        }
    }
}
