using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class NERCTargetAudienceFromIlaMap : Common.MigrationMap<TblCourse, NERCTargetAudience>
    {
        List<TblCourse> _courses;

        List<string> _usedAudiences = new List<string>();

        public NERCTargetAudienceFromIlaMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblCourse> getSourceRecords()
        {
            _courses = (_source as EMP_DemoContext).TblCourses.Where(r => !System.String.IsNullOrEmpty(r.TargetAudienceOtherSpecify.Trim())).ToList();
            return _courses;
        }

        protected override NERCTargetAudience mapRecord(TblCourse obj)
        {

            if (_usedAudiences.Contains(obj.TargetAudienceOtherSpecify)) return null;

            _usedAudiences.Add(obj.TargetAudienceOtherSpecify);

            return new NERCTargetAudience()
            {
                IsOther = true,
                Name = obj.TargetAudienceOtherSpecify,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _courses.Count();
        }

        protected override void updateTarget(NERCTargetAudience record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).NERCTargetAudiences.Add(record);
        }
    }
}
