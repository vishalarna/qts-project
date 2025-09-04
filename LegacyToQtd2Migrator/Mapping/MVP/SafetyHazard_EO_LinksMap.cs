using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SafetyHazard_EO_LinksMap : Common.MigrationMap<TblSafetyHazardEo, SafetyHazard_EO_Link>
    {
        List<TblSafetyHazardEo> _safetyhazardseo;
        public SafetyHazard_EO_LinksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<TblSafetyHazardEo> getSourceRecords()
        {
            _safetyhazardseo = (_source as EMP_DemoContext).TblSafetyHazardEos.ToListAsync().Result;
            return _safetyhazardseo;
        }

        protected override SafetyHazard_EO_Link mapRecord(TblSafetyHazardEo obj)
        {
            return new SafetyHazard_EO_Link()
            {
                SafetyHazardId=obj.Shzid,
                Deleted = false,
                Active = true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _safetyhazardseo.Count();
        }

        protected override void updateTarget(SafetyHazard_EO_Link record)
        {
            (_target as QTD2.Data.QTDContext).SafetyHazard_EO_Links.Add(record);
        }
    }
}
