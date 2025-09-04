using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SegmentObjectiveLinksMap : Common.MigrationMap<TblCourseSegmentLearningObjective, SegmentObjective_Link>
    {
        List<TblCourseSegmentLearningObjective> _segmentObjective;

        public SegmentObjectiveLinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCourseSegmentLearningObjective> getSourceRecords()
        {
            _segmentObjective = (_source as EMP_DemoContext).TblCourseSegmentLearningObjectives.ToListAsync().Result;
            return _segmentObjective;
        }

        protected override SegmentObjective_Link mapRecord(TblCourseSegmentLearningObjective obj)
        {
            return new SegmentObjective_Link()
            {
                Active = true,
                EnablingObjectiveId=obj.ObjId,
                //TaskId,
                //SegmentId
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _segmentObjective.Count();
        }

        protected override void updateTarget(SegmentObjective_Link record)
        {
            (_target as QTD2.Data.QTDContext).SegmentObjective_Links.Add(record);
        }
    }
}
