using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyToQtd2Migrator.Helpers;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class EmployeeCertifictaionHistoriesMap : Common.MigrationMap<TblCertificationHistory, EmployeeCertifictaionHistory>
    {
        List<TblCertificationHistory> _employeeCertificationsHistory;
        List<LkTblCertificationType> _sourceCertificationTypes;
        List<TblEmployee> _sourceEmployees;

        List<Employee> _targetEmployees;
        List<EmployeeCertification> _targetEmployeeCertifications;

        public EmployeeCertifictaionHistoriesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblCertificationHistory> getSourceRecords()
        {
            _employeeCertificationsHistory = (_source as EMP_DemoContext).TblCertificationHistories.ToListAsync().Result;
            _sourceCertificationTypes = (_source as EMP_DemoContext).LkTblCertificationTypes.ToList();
            _sourceEmployees = (_source as EMP_DemoContext).TblEmployees.ToList();

            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.Include("Person").ToList();
            _targetEmployeeCertifications = (_target as QTD2.Data.QTDContext).EmployeeCertifications.Include("Certification").Include("Employee").ToList();
            return _employeeCertificationsHistory;
        }

        protected override EmployeeCertifictaionHistory mapRecord(TblCertificationHistory obj)
        {
            var sourceEmployee = _sourceEmployees.Where(r => r.Eid == obj.Eid).FirstOrDefault();
            var targetEmployee = sourceEmployee == null ? null : _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();

            if (targetEmployee == null) return null;

            var sourceCertificationType = _sourceCertificationTypes.Where(r => r.CertId == obj.ChNerccertArea.GetValueOrDefault()).FirstOrDefault();

            if (sourceCertificationType == null) return null;

            var targetEmployeeCertification = _targetEmployeeCertifications
                    .Where(r => r.EmployeeId == targetEmployee.Id)
                    .Where(r => (r.CertificationNumber.ToUpper() == obj.ChNerccertNum) || (string.IsNullOrEmpty(obj.ChNerccertNum) && r.Certification.CertAcronym.ToUpper() == sourceCertificationType.CertAbbrev.ToUpper()))
                    .FirstOrDefault();

            if (targetEmployeeCertification == null) return null;

            return new EmployeeCertifictaionHistory()
            {
                ChangeEffectiveDate = DateOnly.FromDateTime(obj.ChNerccertIssueDate.GetValueOrDefault()),
                DRADate = DateOnly.FromDateTime(obj.ChDra.GetValueOrDefault()),
                ExpirationDate = DateOnly.FromDateTime(obj.ChNerccertExpDate.GetValueOrDefault()),
                IssueDate = DateOnly.FromDateTime(obj.ChNerccertIssueDate.GetValueOrDefault()),
                CertificationNumber = obj.ChNerccertNum,
                EmployeeCertificationId = targetEmployeeCertification.Id,
                //ChangeNotes = history.note
                //ChangeNotes
                Deleted = false,
                Active = true
            };
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _employeeCertificationsHistory.Count();
        }

        protected override void updateTarget(EmployeeCertifictaionHistory record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).EmployeeCertifictaionHistories.Add(record);
        }
    }
}
