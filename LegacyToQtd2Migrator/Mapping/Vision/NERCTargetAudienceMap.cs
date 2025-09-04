using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Vision.Data;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.Vision
{
    public class NERCTargetAudienceMap : Common.MigrationMap<XrefLibImpl, NERCTargetAudience>
    {
        List<XrefLibImpl> _sourceAudiences;
        int _projectId;

        public NERCTargetAudienceMap(DbContext source, DbContext target, int projectId) : base(source, target)
        {
            _projectId = projectId;
        }
        protected override List<XrefLibImpl> getSourceRecords()
        {
            _sourceAudiences = (_source as VisionContext).XrefLibImpls
                    .Where(r => r.Text.ToUpper().Contains("TARGET AUDIENCE - "))
                    .Where(r => r.FkExpiredBy == null)
                    .Where(r => r.FkProject == _projectId).ToList();

            return _sourceAudiences;
        }

        protected override NERCTargetAudience mapRecord(XrefLibImpl obj)
        {
            return new NERCTargetAudience()
            {
                IsOther = false,
                Name = obj.Text.RtfToPlainText().Split(" - ")[1],
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceAudiences.Count();
        }

        protected override void updateTarget(NERCTargetAudience record)
        {
            if ((_target as QTD2.Data.QTDContext).NERCTargetAudiences.Where(r => r.Name == record.Name).Count() > 0) return;

            (_target as QTD2.Data.QTDContext).NERCTargetAudiences.Add(record);
        }
    }
}
