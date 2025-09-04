using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ProviderLevelsFromlkTbllLAProviderStatusMap : Common.MigrationMap<LkTblIlaproviderStatus, ProviderLevel>
    {
        List<LkTblIlaproviderStatus> _ilaproviderStatuses;
        public ProviderLevelsFromlkTbllLAProviderStatusMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LkTblIlaproviderStatus> getSourceRecords()
        {
            _ilaproviderStatuses = (_source as EMP_DemoContext).LkTblIlaproviderStatuses.ToListAsync().Result;
            return _ilaproviderStatuses;
        }

        protected override ProviderLevel mapRecord(LkTblIlaproviderStatus obj)
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
            TotalRecordsToConvert = _ilaproviderStatuses.Count();
        }
        protected override void updateTarget(ProviderLevel record)
        {
            (_target as QTD2.Data.QTDContext).ProviderLevels.Add(record);
        }
    }
}
