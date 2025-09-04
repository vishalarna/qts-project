using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;

using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Certifications.Interfaces;

namespace QTD2.Domain.Certifications.Implimentations.FulfillmentCalculators
{
    public class BasicCertificationFulfillmentCalculator : ICertificationFulfillmentCalculator
    {
        IClassScheduleEmployeeService _classScheduleEmployeeService;
        ICertificationCalculatorRequestRecordHelper _certificationCalculatorRequestRecordHelper;

        public BasicCertificationFulfillmentCalculator(
            IClassScheduleEmployeeService classScheduleEmployeeService,
            ICertificationCalculatorRequestRecordHelper certificationCalculatorRequestRecordHelper
            )
        {
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _certificationCalculatorRequestRecordHelper = certificationCalculatorRequestRecordHelper;
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var certificationCalculatorRequestRecords = await _certificationCalculatorRequestRecordHelper.GetCertificationCalculatorRequestRecords(employeeIds, certificationIds);

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForCertificationCalculationAsync(employeeIds);

            List<CertificationFulfillmentStatus> records = buildRecords(certificationCalculatorRequestRecords, classScheduleEmployees);

            return records;
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesAsync(List<int> employeeCertificationIds)
        {
            var certificationCalculatorRequestRecords = await _certificationCalculatorRequestRecordHelper.GetCertificationCalculatorRequestRecords(employeeCertificationIds);

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForNercCertificationCalculationAsync(certificationCalculatorRequestRecords.Select(c => c.Employee.Id).Distinct().ToList());
            
            List<CertificationFulfillmentStatus> records = buildRecords(certificationCalculatorRequestRecords, classScheduleEmployees);

            return records;
        }

        public Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWhenRelatedEmployeeCertificationExistsAsync(List<int> employeeIds, List<int> certificationIds, List<int> relatedCertificationIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForDateAsync(List<int> employeeIds, List<int> certificationIds, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWithinEmployeeCertificationDateRangeAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var records = await GetFulfillmentStatusesAsync(employeeIds, certificationIds);

            foreach (var record in records)
            {
                record.FulfillmentRecords = record.FulfillmentRecords.Where(r => DateOnly.FromDateTime(r.ClassCompletionDate.GetValueOrDefault()) >= record.IssueDate && DateOnly.FromDateTime(r.ClassCompletionDate.GetValueOrDefault()) <= record.ExpirationDate).ToList();
            }

            return records;
        }
        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForYearAsync(List<int> employeeIds, List<int> certificationIds, int year)
        {
            var certificationCalculatorRequestRecords = await _certificationCalculatorRequestRecordHelper.GetCertificationCalculatorRequestRecordsForYear(employeeIds, certificationIds, year, true, true);

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForNercCertificationCalculationAsync(employeeIds);

            List<CertificationFulfillmentStatus> records = buildRecords(certificationCalculatorRequestRecords, classScheduleEmployees);

            return records;
        }

        protected List<CertificationFulfillmentStatus> buildRecords(List<CertificationCalculatorRequestRecord> certificationCalculatorRequestRecords, List<ClassSchedule_Employee> classScheduleEmployees)
        {
            var certificationFulfillmentStatuses = new List<CertificationFulfillmentStatus>();

            foreach (var certificationCalculatorRequestRecord in certificationCalculatorRequestRecords)
            {
                var certificationFulfillmentStatus = new CertificationFulfillmentStatus(certificationCalculatorRequestRecord);

                var classScheduleEmployeesFiltered = classScheduleEmployees
                        .Where(r => r.EmployeeId == certificationCalculatorRequestRecord.Employee.Id)
                        .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) >= (certificationCalculatorRequestRecord.UseIssueDate ? certificationCalculatorRequestRecord.IssueDate : certificationCalculatorRequestRecord.RenewalDate))
                        .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) < certificationCalculatorRequestRecord.ExpirationDate)
                        .ToList();

                foreach (var classScheduleEmployee in classScheduleEmployeesFiltered)
                {
                    foreach (var ilaCertificationLink in classScheduleEmployee.ClassSchedule.ILA.ILACertificationLinks)
                    {
                        if (ilaCertificationLink.CertificationId != certificationCalculatorRequestRecord.Certification.Id) { continue; }

                        bool awardsCehs = getAwardsCehs(certificationCalculatorRequestRecord, classScheduleEmployeesFiltered, ilaCertificationLink, classScheduleEmployee);

                        bool pendingAwardsCehs = getPendingAwardsCehs(certificationCalculatorRequestRecord, classScheduleEmployeesFiltered, ilaCertificationLink, classScheduleEmployee);

                        var certificationFulfillmentRecord = new CertificationFulfillmentRecord(classScheduleEmployee, ilaCertificationLink, awardsCehs, pendingAwardsCehs);

                        certificationFulfillmentStatus.FulfillmentRecords.Add(certificationFulfillmentRecord);
                    }

                }
                certificationFulfillmentStatuses.Add(certificationFulfillmentStatus);
            }

            return certificationFulfillmentStatuses;
        }

        private bool getAwardsCehs(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, List<ClassSchedule_Employee> classScheduleEmployees, ILACertificationLink ilaCertificationLink, ClassSchedule_Employee classScheduleEmployee)
        {
            return true;
        }

        private bool getPendingAwardsCehs(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, List<ClassSchedule_Employee> classScheduleEmployees, ILACertificationLink ilaCertificationLink, ClassSchedule_Employee classScheduleEmployee)
        {
            return true;
        }
    }
}
