using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class CoversheetsMap : Common.MigrationMap<TblCoversheet, Coversheet>
    {
        List<TblCoversheet> _coversheets;
        public CoversheetsMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblCoversheet> getSourceRecords()
        {
            _coversheets = (_source as EMP_DemoContext).TblCoversheets.ToListAsync().Result;
            return _coversheets;
        }

        protected override Coversheet mapRecord(TblCoversheet obj)
        {
            return new Coversheet()
            {
                CoversheetTitle=obj.Cvtitle,
                CoversheetTypeId=obj.CvtypeId,
                CoversheetInstructions=obj.Cvinstructions,
                Deleted=false,
                Active=true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _coversheets.Count();
        }

        protected override void updateTarget(Coversheet record)
        {
            (_target as QTD2.Data.QTDContext).Coversheets.Add(record);
        }

    }
}
