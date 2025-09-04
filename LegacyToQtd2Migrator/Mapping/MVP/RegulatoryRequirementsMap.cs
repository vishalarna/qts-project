using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class RegulatoryRequirementsMap : Common.MigrationMap<TblNercstandard, RegulatoryRequirement>
    {
        List<TblNercstandard> _nercStandards;

        public RegulatoryRequirementsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblNercstandard> getSourceRecords()
        {
            _nercStandards = (_source as EMP_DemoContext).TblNercstandards.ToList();
            return _nercStandards;
        }

        protected override RegulatoryRequirement mapRecord(TblNercstandard obj)
        {
            return new RegulatoryRequirement()
            {
                Active = true,
                IssuingAuthorityId = 1,
                //RevisionNumber = obj.
                Deleted = false,
                Title = obj.Nsname,
                Number = obj.Nsname,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _nercStandards.Count();
        }

        protected override void updateTarget(RegulatoryRequirement record)
        {
            (_target as QTD2.Data.QTDContext).RegulatoryRequirements.Add(record);
        }
    }
}
