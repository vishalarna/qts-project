using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;


using QTD2.Domain.Certifications.Models;
using System.Runtime.Intrinsics.X86;
using QTD2.Domain.Services.Core;
using Microsoft.Extensions.DependencyInjection;
using QTD2.Domain.Certifications.Interfaces;

namespace QTD2.Domain.Certifications.Implimentations.FulfillmentCalculators
{
    public class NercCertificationFulfillmentCalculator : ICertificationFulfillmentCalculator
    {
        IClassScheduleEmployeeService _classScheduleEmployeeService;

        ICertificationCalculatorRequestRecordHelper _certificationCalculatorRequestRecordHelper;

        public NercCertificationFulfillmentCalculator(
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

        public Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWhenRelatedEmployeeCertificationExistsAsync(List<int> employeeIds, List<int> certificationIds, List<int> relatedCertificationIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesForDateAsync(List<int> employeeIds, List<int> certificationIds, DateTime date)
        {
            var certificationCalculatorRequestRecords = await _certificationCalculatorRequestRecordHelper.GetCertificationCalculatorRequestRecordsByDate(employeeIds, certificationIds, date, true);

            List<ClassSchedule_Employee> classScheduleEmployees = await _classScheduleEmployeeService.GetForNercCertificationCalculationAsync(employeeIds);

            List<CertificationFulfillmentStatus> records = buildRecords(certificationCalculatorRequestRecords, classScheduleEmployees);

            return records;
        }

        public async Task<List<CertificationFulfillmentStatus>> GetFulfillmentStatusesWithinEmployeeCertificationDateRangeAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var records = await GetFulfillmentStatusesAsync(employeeIds, certificationIds);

            foreach (var record in records)
            {
                record.FulfillmentRecords = record.FulfillmentRecords.Where(r => r.ClassCompletionDate.HasValue && DateOnly.FromDateTime(r.ClassCompletionDate.Value) >= record.IssueDate && DateOnly.FromDateTime(r.ClassCompletionDate.Value) <= record.ExpirationDate).ToList();
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

                var startDate = getStartDate(certificationCalculatorRequestRecord);
                var endDate = getEndDate(certificationCalculatorRequestRecord, startDate);

                var classScheduleEmployeesFiltered = classScheduleEmployees
                        .Where(r => r.EmployeeId == certificationCalculatorRequestRecord.Employee.Id)
                        .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) >= startDate)
                        .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) < endDate)
                        .OrderByDescending(r => r.IsComplete)
                        .ToList();

                foreach (var classScheduleEmployee in classScheduleEmployeesFiltered)
                {
                    var partialCredits = classScheduleEmployee.ClassScheduleEmployee_ILACertificationLink_PartialCredits ?? new List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>();

                    foreach (var ilaCertificationLink in classScheduleEmployee.ClassSchedule.ILA.ILACertificationLinks)
                    {
                        if (ilaCertificationLink.CertificationId != certificationCalculatorRequestRecord.Certification.Id) { continue; }

                        bool awardsCehs = getAwardsCehs(certificationCalculatorRequestRecord, classScheduleEmployeesFiltered, ilaCertificationLink, classScheduleEmployee);

                        bool pendingAwardsCehs = getPendingAwardsCehs(certificationCalculatorRequestRecord, classScheduleEmployeesFiltered, ilaCertificationLink, classScheduleEmployee);

                        var partialCredit = partialCredits.FirstOrDefault(pc => pc.ILACertificationLinkId == ilaCertificationLink.Id);

                        var certificationFulfillmentRecord = new CertificationFulfillmentRecord(classScheduleEmployee, ilaCertificationLink, awardsCehs, pendingAwardsCehs, partialCredit);
                        
                        certificationFulfillmentStatus.FulfillmentRecords.Add(certificationFulfillmentRecord);
                    }
                }
                certificationFulfillmentStatuses.Add(certificationFulfillmentStatus);
            }

            return certificationFulfillmentStatuses;
        }

        private bool getAwardsCehs(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, List<ClassSchedule_Employee> classScheduleEmployees, ILACertificationLink ilaCertificationLink, ClassSchedule_Employee classScheduleEmployee)
        {
            var fails = new List<string>() { "F", "O" };

            if (fails.Contains(classScheduleEmployee.FinalGrade)) return false;

            var startDate = getStartDate(certificationCalculatorRequestRecord);
            var endDate = getEndDate(certificationCalculatorRequestRecord, startDate);
            var fullAndPartialYears = getCountOfFullAndPartialYearsBetweenDates(startDate, endDate);

            var count = 0;
            for (int i = 0; i <= fullAndPartialYears - 1; i++)
            {
                var currentStartDate = startDate.AddYears(i);
                var currentEndDate = endDate < currentStartDate.AddYears(1) ? endDate : currentStartDate.AddYears(1);

                count = 0;
                var cses = classScheduleEmployees
                                            .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) >= currentStartDate)
                                            .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) < currentEndDate)
                                            .Where(r => r.ClassSchedule.ILAID == ilaCertificationLink.ILAId)
                                            .Where(r => !fails.Contains(r.FinalGrade))
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

        private bool getPendingAwardsCehs(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, List<ClassSchedule_Employee> classScheduleEmployees, ILACertificationLink ilaCertificationLink, ClassSchedule_Employee classScheduleEmployee)
        {
            var fails = new List<string>() { "F", "O" };
            if (fails.Contains(classScheduleEmployee.FinalGrade)) return false;

            var startDate = getStartDate(certificationCalculatorRequestRecord);
            var endDate = getEndDate(certificationCalculatorRequestRecord, startDate);
            var fullAndPartialYears = getCountOfFullAndPartialYearsBetweenDates(startDate, endDate);

            var count = 0;
            for (int i = 0; i <= fullAndPartialYears - 1; i++)
            {
                var currentStartDate = startDate.AddYears(i);
                var currentEndDate = endDate < currentStartDate.AddYears(1) ? endDate : currentStartDate.AddYears(1);

                count = 0;
                var cses = classScheduleEmployees
                                            .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) >= currentStartDate)
                                            .Where(r => DateOnly.FromDateTime(r.ClassSchedule.EndDateTime) < currentEndDate)
                                            .Where(r => r.ClassSchedule.ILAID == ilaCertificationLink.ILAId)
                                            .Where(r => !fails.Contains(classScheduleEmployee.FinalGrade))
                                            // Only keep future incomplete classes and complete classes
                                            // this way, we still consider finished classes before pending classes when pendingly awarding hours
                                            .Where(r => (!r.IsComplete && r.ClassSchedule.EndDateTime >= DateTime.UtcNow) || r.IsComplete)
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

        private int getCountOfFullAndPartialYearsBetweenDates(DateOnly start, DateOnly end)
        {
            if (start >= end)
            {
                return 0;
            }

            int totalYears = 0;

            while (start.AddYears(totalYears) < end)
            {
                totalYears++;
            }

            return totalYears;
        }

        private DateOnly getStartDate(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord) {
            var startDate = (certificationCalculatorRequestRecord.UseIssueDate ? certificationCalculatorRequestRecord.IssueDate : certificationCalculatorRequestRecord.RenewalDate) ?? certificationCalculatorRequestRecord.IssueDate;
            return startDate;
        }

        private DateOnly getEndDate(CertificationCalculatorRequestRecord certificationCalculatorRequestRecord, DateOnly startDate)
        {
            var endDate = certificationCalculatorRequestRecord.ExpirationDate ?? startDate.AddYears(certificationCalculatorRequestRecord.Certification.RenewalInterval ?? 0);
            if (certificationCalculatorRequestRecord.IsFromEmployeeCertification)
            {
                endDate = endDate.AddYears(1);
            }
            return endDate;
        }
    }
}
