using LegacyToQtd2Migrator.Helpers;
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
    public class EmployeeCertificationsMap : Common.MigrationMap<TblCertificationHistory, EmployeeCertification>
    {
        List<TblCertificationHistory> _employeeCertifications;

        public EmployeeCertificationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCertificationHistory> getSourceRecords()
        {
            _employeeCertifications = (_source as EMP_DemoContext).TblCertificationHistories.ToListAsync().Result;
            return _employeeCertifications;
        }

        protected override EmployeeCertification mapRecord(TblCertificationHistory obj)
        {
            return new EmployeeCertification()
            {
                Active = true,
                EmployeeId=obj.Eid??-1,
                //CertificationId,
                //IssueDate=obj.ChNerccertIssueDate,
                ExpirationDate= DateOnly.FromDateTime(obj.ChNerccertExpDate.GetValueOrDefault()),
                //RenewalDate,
                //RollOverHours,
                //CertificationNumber






            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employeeCertifications.Count();
        }

        protected override void updateTarget(EmployeeCertification record)
        {
            (_target as QTD2.Data.QTDContext).EmployeeCertifications.Add(record);
        }
    }
}
