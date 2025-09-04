using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class NERCTargetAudienceMap : Common.MigrationMap<TblTargetAudience, NERCTargetAudience>
    {
        List<TblTargetAudience> _targetAudiences;

        public NERCTargetAudienceMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblTargetAudience> getSourceRecords()
        {
            _targetAudiences = (_source as EMP_DemoContext).TblTargetAudiences.ToList();
            return _targetAudiences;
        }

        protected override NERCTargetAudience mapRecord(TblTargetAudience obj)
        {           

            return new NERCTargetAudience()
            { 
                IsOther = false,
                Name = obj.TargetAudience,
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _targetAudiences.Count();
        }

        protected override void updateTarget(NERCTargetAudience record)
        {
            (_target as QTD2.Data.QTDContext).NERCTargetAudiences.Add(record);
        }
    }
}
