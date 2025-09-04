using QTD2.Domain.Certifications.Interfaces;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QTD2.Domain.Certifications.Implimentations
{
    public class CertificationCalculatorRequestRecordHelper : ICertificationCalculatorRequestRecordHelper
    {
        IEmployeeCertificationService _employeeCertificationService;
        IEmployeeCertificationHistoryService _employeeCertificationHistoryService;
        ICertificationService _certificationService;
        IEmployeeService _employeeService;

        public CertificationCalculatorRequestRecordHelper(
            IEmployeeCertificationService employeeCertificationService,
            IEmployeeCertificationHistoryService employeeCertificationHistoryService,
            ICertificationService certificationService,
            IEmployeeService employeeService
            )
        {
            _employeeCertificationService = employeeCertificationService;
            _employeeCertificationHistoryService = employeeCertificationHistoryService;
            _certificationService = certificationService;
            _employeeService = employeeService;
        }

        public async Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecords(List<int> employeeIds, List<int> certificationIds)
        {
            List<EmployeeCertification> employeeCertifications = await _employeeCertificationService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            List<EmployeeCertifictaionHistory> employeeCertifictaionHistories = await _employeeCertificationHistoryService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            List<CertificationCalculatorRequestRecord> certificationCalculatorRequestRecords = employeeCertifications
                .Select(ec => new CertificationCalculatorRequestRecord(ec, !employeeCertifictaionHistories.Any(ech => ech.EmployeeCertification.EmployeeId == ec.EmployeeId && ech.EmployeeCertification.CertificationId == ec.CertificationId)))
                .ToList();

            

            return certificationCalculatorRequestRecords;
        }

        public async Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecords(List<int> employeeCertificationIds)
        {
            List<EmployeeCertification> employeeCertifications = await _employeeCertificationService.GetEmployeeCertificationByEmployeeCertIds(employeeCertificationIds);
            List<int> employeeIds = employeeCertifications.Select(x => x.EmployeeId).Distinct().ToList();
            List<int> certificationIds = employeeCertifications.Select(x => x.CertificationId).Distinct().ToList();

            var result = await GetCertificationCalculatorRequestRecords(employeeIds, certificationIds);

            // Limit to original EmployeeCertification set in case crossovers of the Employee and Cert IDs existed
            return result.Where(r => employeeCertifications.Any(employeeCertifications => employeeCertifications.EmployeeId == r.Employee.Id && employeeCertifications.CertificationId == r.Certification.Id)).ToList();
        }

        public async Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecordsByDate(List<int> employeeIds, List<int> certificationIds, DateTime date, bool includeHistoryRecords = false)
        {
            List<EmployeeCertification> employeeCertifications = await _employeeCertificationService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            List<EmployeeCertifictaionHistory> employeeCertifictaionHistories = await _employeeCertificationHistoryService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            var filteredEmployeeCertifications = employeeCertifications.Where(ec => (employeeCertifictaionHistories.Any(ech => ech.EmployeeCertification.EmployeeId == ec.EmployeeId && ech.EmployeeCertification.CertificationId == ec.CertificationId) ? ec.RenewalDate : ec.IssueDate) <= DateOnly.FromDateTime(date) && DateOnly.FromDateTime(date) < ec.ExpirationDate?.AddYears(1)).ToList();

            List<CertificationCalculatorRequestRecord> certificationCalculatorRequestRecords = filteredEmployeeCertifications
                .Select(ec => new CertificationCalculatorRequestRecord(ec, !employeeCertifictaionHistories.Any(ech => ech.EmployeeCertification.EmployeeId == ec.EmployeeId && ech.EmployeeCertification.CertificationId == ec.CertificationId)))
                .ToList();

            if (includeHistoryRecords)
            {
                var filteredHistoryRecords = employeeCertifictaionHistories.Where(ech => ech.IssueDate <= DateOnly.FromDateTime(date) && DateOnly.FromDateTime(date) < ech.ExpirationDate && !filteredEmployeeCertifications.Any(ec => ec.Id == ech.EmployeeCertificationId)).ToList();

                certificationCalculatorRequestRecords.AddRange(
                    filteredHistoryRecords
                    .Select(ech =>
                        new CertificationCalculatorRequestRecord(
                            ech,
                            employeeCertifications.FirstOrDefault(ec => ec.Id == ech.EmployeeCertificationId)?.RollOverHours ?? 0) // Get RolloverHours from EmployeeCertification because its not stored on EmployeeCertificationHistory
                        )
                    .ToList()
                );
            }

            return certificationCalculatorRequestRecords;
        }

        public async Task<List<CertificationCalculatorRequestRecord>> GetCertificationCalculatorRequestRecordsForYear(List<int> employeeIds, List<int> certificationIds, int year, bool includeHistoryRecords = false, bool generateDummyRecords = false)
        {
            List<EmployeeCertification> employeeCertifications = await _employeeCertificationService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            List<EmployeeCertifictaionHistory> employeeCertifictaionHistories = await _employeeCertificationHistoryService.GetEmployeeCertificationsByEmployeeAndTypeAsync(employeeIds, certificationIds);

            var filteredEmployeeCertifications = employeeCertifications.Where(ec => (employeeCertifictaionHistories.Any(ech => ech.EmployeeCertification.EmployeeId == ec.EmployeeId && ech.EmployeeCertification.CertificationId == ec.CertificationId) ? ec.RenewalDate : ec.IssueDate).GetValueOrDefault().Year <= year && year <= ec.ExpirationDate.GetValueOrDefault().Year).ToList();

            List<CertificationCalculatorRequestRecord> certificationCalculatorRequestRecords = filteredEmployeeCertifications
                .Select(ec => new CertificationCalculatorRequestRecord(ec, !employeeCertifictaionHistories.Any(ech => ech.EmployeeCertification.EmployeeId == ec.EmployeeId && ech.EmployeeCertification.CertificationId == ec.CertificationId)))
                .ToList();

            if (includeHistoryRecords)
            {
                var filteredHistoryRecords = employeeCertifictaionHistories.Where(ech => ech.IssueDate.Year <= year && year <= ech.ExpirationDate.Year).ToList();

                certificationCalculatorRequestRecords.AddRange(
                    filteredHistoryRecords
                    .Select(ech =>
                        new CertificationCalculatorRequestRecord(
                            ech,
                            employeeCertifications.FirstOrDefault(ec => ec.Id == ech.EmployeeCertificationId)?.RollOverHours ?? 0) // Get RolloverHours from EmployeeCertification because its not stored on EmployeeCertificationHistory
                        )
                    .ToList()
                );
            }

            if (generateDummyRecords)
            {
                //Arbitrarily use first Certification, this should only be used when wanting one result record anyway (so multiple NERC CertIds will only make one dummy NERC record here)
                var certification = (await _certificationService.GetWithCertifyingBodyAndReqsByIdAsync(new List<int>() { certificationIds[0] })).FirstOrDefault();

                if (certification == null) return certificationCalculatorRequestRecords;

                foreach (var employeeId in employeeIds)
                {
                    // Generate dummy record only for Employees where we didn't find a certificationCalculatorRequestRecord
                    if (!certificationCalculatorRequestRecords.Any(ccrr => ccrr.Employee.Id == employeeId))
                    {
                        var employee = await _employeeService.GetEmployeeWithOrgAndPosAsync(employeeId);

                        certificationCalculatorRequestRecords.Add(new CertificationCalculatorRequestRecord(employee, certification, new DateOnly(year, 1, 1), new DateOnly(year, 1, 1).AddYears(3)));
                    }
                }
            }

            return certificationCalculatorRequestRecords;
        }
    }
}
