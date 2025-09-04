using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class SaftyHazard_AbatementsMap : Common.MigrationMap<TblSafetyHazardAbatement, SaftyHazard_Abatement>
    {
        List<TblSafetyHazardAbatement> _saftyhazardabatment;
        public SaftyHazard_AbatementsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        

        protected override List<TblSafetyHazardAbatement> getSourceRecords()
        {
            _saftyhazardabatment = (_source as EMP_DemoContext).TblSafetyHazardAbatements.ToListAsync().Result;
            return _saftyhazardabatment;
        }

        protected override SaftyHazard_Abatement mapRecord(TblSafetyHazardAbatement obj)
        {
            return new SaftyHazard_Abatement()
            {
                Active = true,
                Deleted = false,
                SaftyHazardId=obj.Shzid,
                Number=obj.Anum,
                Description=obj.Shzabatement
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _saftyhazardabatment.Count();
        }

        protected override void updateTarget(SaftyHazard_Abatement record)
        {
            (_target as QTD2.Data.QTDContext).SaftyHazard_Abatements.Add(record);
        }
    
}
}
