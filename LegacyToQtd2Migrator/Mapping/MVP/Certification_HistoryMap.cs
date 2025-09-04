using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Certification_HistoryMap : Common.MigrationMap<TblCertificationHistory, Certification_History>
    {
        List<TblCertificationHistory> _tools;

        public Certification_HistoryMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCertificationHistory> getSourceRecords()
        {
            _tools = (_source as EMP_DemoContext).TblCertificationHistories.ToListAsync().Result;
            return _tools;
        }

        protected override Certification_History mapRecord(TblCertificationHistory obj)
        {
            return new Certification_History()
            {
                Active = true,
                //CertId
                //EffectiveDate
                //Notes


            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _tools.Count();
        }

        protected override void updateTarget(Certification_History record)
        {
            //(_target as QTD2.Data.QTDContext).cert.Add(record);
        }
    }
}
