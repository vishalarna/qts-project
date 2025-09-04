using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ProviderLevelsFromlkTblSupplierMap : Common.MigrationMap<LktblSupplier, ProviderLevel>
    {
        List<LktblSupplier> _Suppliers;
        public ProviderLevelsFromlkTblSupplierMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LktblSupplier> getSourceRecords()
        {
            _Suppliers = (_source as EMP_DemoContext).LktblSuppliers.ToListAsync().Result;
            return _Suppliers;
        }

        protected override ProviderLevel mapRecord(LktblSupplier obj)
        {
            return new ProviderLevel()
            {
                //Name
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted = false,
                Active = true,

            };
        }
        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _Suppliers.Count();
        }
        protected override void updateTarget(ProviderLevel record)
        {
            (_target as QTD2.Data.QTDContext).ProviderLevels.Add(record);
        }
    }
}
