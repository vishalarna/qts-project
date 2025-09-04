using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Proc_IssuingAuthority_HistoriesMap : Common.MigrationMap<LktblIssuingAuthority, Proc_IssuingAuthority_History>
    {
        List<LktblIssuingAuthority> _procIssuingAuthorityHistory;
        public Proc_IssuingAuthority_HistoriesMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<LktblIssuingAuthority> getSourceRecords()
        {
            _procIssuingAuthorityHistory = (_source as EMP_DemoContext).LktblIssuingAuthorities.ToListAsync().Result;
            return _procIssuingAuthorityHistory;
        }

        protected override Proc_IssuingAuthority_History mapRecord(LktblIssuingAuthority obj)
        {
            return new Proc_IssuingAuthority_History()
            {
                Active = true,
               //ProcedureIssuingAuthorityId,
               //OldStatus,
               //NewStatus,
               //ChangeEffectiveDate,
               //ChangeNotes

            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _procIssuingAuthorityHistory.Count();
        }

        protected override void updateTarget(Proc_IssuingAuthority_History record)
        {
            (_target as QTD2.Data.QTDContext).Proc_IssuingAuthority_Histories.Add(record);
        }

    }
}
