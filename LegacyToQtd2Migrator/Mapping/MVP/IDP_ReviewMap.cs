using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class IDP_ReviewMap  : Common.MigrationMap<TblIdp, IDP_Review>
    {
        List<TblIdp> _idp;
        public IDP_ReviewMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblIdp> getSourceRecords()
        {
            _idp = (_source as EMP_DemoContext).TblIdps.ToListAsync().Result;
            return _idp;
        }

        protected override IDP_Review mapRecord(TblIdp obj)
        {
            return new IDP_Review()
            {
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _idp.Count();
        }

        protected override void updateTarget(IDP_Review record)
        {
            //(_target as QTD2.Data.QTDContext).Add(record);
        }
    }
}
