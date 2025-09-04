using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
  public  class SaftyHazardsMap :Common.MigrationMap<TblSafetyHazard, SaftyHazard>
    {
        List<TblSafetyHazard> _saftyhazard;

        public SaftyHazardsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblSafetyHazard> getSourceRecords()
        {
            _saftyhazard = (_source as EMP_DemoContext).TblSafetyHazards.ToListAsync().Result;
            return _saftyhazard;
        }

        protected override SaftyHazard mapRecord(TblSafetyHazard obj)
        {
            var category = (_target as QTD2.Data.QTDContext).SaftyHazard_Categories.Where(r => r.Number == obj.Hzcid).First();

            return new SaftyHazard()
            {
                Active = true,
                Deleted = false,
                Title = obj.Shztitle,
                Number = obj.Shznum,
                //EffectiveDate,
                SaftyHazardCategoryId = category.Id
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _saftyhazard.Count();
        }

        protected override void updateTarget(SaftyHazard record)
        {
            (_target as QTD2.Data.QTDContext).SaftyHazards.Add(record);
        }
    }
}
