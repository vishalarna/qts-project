using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;


using QTD2.Domain.Certifications.Models;
using System.Runtime.Intrinsics.X86;
using QTD2.Domain.Certifications.Interfaces;
using QTD2.Domain.Services.Core;

namespace QTD2.Domain.Certifications.Implimentations.FulfillmentCalculators
{
    public class EmergencyResponseCertificationFulfillmentCalculator : ICertificationFulfillmentCalculator
    {
        IClassScheduleEmployeeService _classScheduleEmployeeService;
        ICertificationCalculatorRequestRecordHelper _certificationCalculatorRequestRecordHelper;

        public EmergencyResponseCertificationFulfillmentCalculator(
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

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForNercCertificationCalculationAsync(employeeIds);

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

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWhenRelatedEmployeeCertificationExistsAsync(List<int> employeeIds, List<int> certificationIds, List<int> relatedCertificationIds)
        {
            var certificationCalculatorRequestRecords = await _certificationCalculatorRequestRecordHelper.GetCertificationCalculatorRequestRecords(employeeIds, relatedCertificationIds);

            employeeIds = certificationCalculatorRequestRecords.Select(ec => ec.Employee.Id).Distinct().ToList();

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForEmergencyResponseCertificationCalculationAsync(employeeIds, certificationIds);

            List<CertificationFulfillmentStatus> records = buildRecords(certificationCalculatorRequestRecords, classScheduleEmployees);

            return records;
        }

        public Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForDateAsync(List<int> employeeIds, List<int> certificationIds, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWithinEmployeeCertificationDateRangeAsync(List<int> employeeIds, List<int> certificationIds)
        {
            throw new NotImplementedException();
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
            var endDate = DateTime.UtcNow;
            var startDate = new DateTime(endDate.Year, 1, 1);

            var certificationFulfillmentStatuses = new List<CertificationFulfillmentStatus>();

            foreach (var certificationCalculatorRequestRecord in certificationCalculatorRequestRecords)
            {
                var certificationFulfillmentStatus = new CertificationFulfillmentStatus(certificationCalculatorRequestRecord);

                var classScheduleEmployeesFiltered = classScheduleEmployees
                        .Where(r => r.EmployeeId == certificationCalculatorRequestRecord.Employee.Id)
                        .Where(r => r.CompletionDate >= startDate)
                        .Where(r => r.CompletionDate < endDate)
                        .ToList();

                foreach (var classScheduleEmployee in classScheduleEmployeesFiltered)
                {
                    foreach (var ilaCertificationLink in classScheduleEmployee.ClassSchedule.ILA.ILACertificationLinks)
                    {
                        if (ilaCertificationLink.CertificationId != certificationCalculatorRequestRecord.Certification.Id) { continue; }

                        bool awardsCehs = getAwardsCehs(certificationCalculatorRequestRecord, classScheduleEmployeesFiltered, ilaCertificationLink, classScheduleEmployee, startDate);

                        bool pendingAwardsCehs = getPendingAwardsCehs(classScheduleEmployee);

                        var certificationFulfillmentRecord = new CertificationFulfillmentRecord(classScheduleEmployee, ilaCertificationLink, awardsCehs, pendingAwardsCehs);

                        certificationFulfillmentStatus.FulfillmentRecords.Add(certificationFulfillmentRecord);
                    }
                }
                certificationFulfillmentStatuses.Add(certificationFulfillmentStatus);
            }

            return certificationFulfillmentStatuses;
        }

        private bool getAwardsCehs(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, List<ClassSchedule_Employee> classScheduleEmployees, ILACertificationLink ilaCertificationLink, ClassSchedule_Employee classScheduleEmployee, DateTime startDate)
        {
            var fails = new List<string>() { "F", "O" };
            var renewalPeriod = certificationCalculatorRequestRecord.Certification.RenewalInterval;

            if (fails.Contains(classScheduleEmployee.FinalGrade)) return false;

            var count = 0;
            for (int i = 0; i <= renewalPeriod - 1; i++)
            {
                count = 0;
                var cses = classScheduleEmployees
                                            .Where(r => r.ClassSchedule.EndDateTime >= startDate.AddYears(i))
                                            .Where(r => r.ClassSchedule.EndDateTime < startDate.AddYears(i + 1))
                                            .Where(r => r.ClassSchedule.ILAID == ilaCertificationLink.ILAId)
                                            .Where(r => !fails.Contains(classScheduleEmployee.FinalGrade))
                                            .OrderByDescending(r => r.IsComplete)
                                            .ThenBy(r => r.ClassSchedule.EndDateTime);
                foreach (var cse in cses)
                {
                    if (cse.Id == classScheduleEmployee.Id && count < 2 && ilaCertificationLink.IsEmergencyOpHours)
                        return true;

                    if (cse.Id == classScheduleEmployee.Id && count < 1)
                        return true;

                    count++;
                }
            }

            return false;
        }
        private bool getPendingAwardsCehs(ClassSchedule_Employee classScheduleEmployee)
        {
            return true;
        }
    }
}