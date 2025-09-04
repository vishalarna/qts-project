using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_NercStandard_LinksMap:Common.MigrationMap<TblIlaNercstandard, ILA_NercStandard_Link>
    {
        List<TblIlaNercstandard> _ilanercstandard;
        public ILA_NercStandard_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblIlaNercstandard> getSourceRecords()
        {
            _ilanercstandard = (_source as EMP_DemoContext).TblIlaNercstandards.ToListAsync().Result;
            return _ilanercstandard;
        }

        protected override ILA_NercStandard_Link mapRecord(TblIlaNercstandard obj)
        {
  

            return new ILA_NercStandard_Link()
            {
                //ILAId
                //StdId
                //NERCStdMemberId
                //CreditHoursByStd
                //CreatedBy
                //CreatedDate
                //ModifiedBy
                //ModifiedDate
                Deleted=false,
                Active=true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilanercstandard.Count();
        }

        protected override void updateTarget(ILA_NercStandard_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_NercStandard_Links.Add(record);
        }
    }
}
