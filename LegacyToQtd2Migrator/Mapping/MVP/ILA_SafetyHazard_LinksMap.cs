using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
   public class ILA_SafetyHazard_LinksMap : Common.MigrationMap<TblSafetyHazardIla, ILA_SafetyHazard_Link>
    {
        List<TblSafetyHazardIla> _safetyhazardila;
        public ILA_SafetyHazard_LinksMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblSafetyHazardIla> getSourceRecords()
        {
            _safetyhazardila = (_source as EMP_DemoContext).TblSafetyHazardIlas.ToListAsync().Result;
            return _safetyhazardila;
        }

        protected override ILA_SafetyHazard_Link mapRecord(TblSafetyHazardIla obj)
        {
            var sourceIla = obj.Cor;
            var sourceSh = obj.Shz;

            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceIla.Cornum).First();
            var targetSh = (_target as QTD2.Data.QTDContext).SaftyHazards.Where(r => r.Title == sourceSh.Shztitle).First();

            return new ILA_SafetyHazard_Link()
            {
                ILAId = targetIla.Id,
                //CreatedBy
                //CreatedDate,
                //ModifiedBy,
                //ModifiedDate
                SafetyHazardId= targetSh.Id,
                Deleted = false,
                Active = true,
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _safetyhazardila.Count();
        }

        protected override void updateTarget(ILA_SafetyHazard_Link record)
        {
            (_target as QTD2.Data.QTDContext).ILA_SafetyHazard_Links.Add(record);
        }
    }
}
