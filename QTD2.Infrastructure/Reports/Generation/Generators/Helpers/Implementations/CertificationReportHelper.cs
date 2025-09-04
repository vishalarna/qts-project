using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators.Helpers
{
    public class CertificationReportHelper : ICertificationReportHelper
    {
        private readonly IEmployeeCertificationService _employeeCertificationService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;

        public CertificationReportHelper(
            IEmployeeCertificationService employeeCertificationService,
            ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory)
        {
            _employeeCertificationService = employeeCertificationService;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
        }

        public async Task<List<CertificationFulfillmentStatus>> GetCertificationFulfillmentStatuses(List<int> employeeIds, List<int> certificationIds)
        {
            var employeeCertifications = (await _employeeCertificationService.FindWithIncludeAsync(ec => employeeIds.Contains(ec.EmployeeId) && certificationIds.Contains(ec.CertificationId), new[] { "Certification.CertifyingBody" })).ToList();

            return await GenerateCertificationFulfillmentStatuses(employeeCertifications);
        }

        public async Task<List<CertificationFulfillmentStatus>> GetCertificationFulfillmentStatuses(List<int> employeeIds)
        {
            var employeeCertifications = (await _employeeCertificationService.FindWithIncludeAsync(ec => employeeIds.Contains(ec.EmployeeId), new[] { "Certification.CertifyingBody" })).ToList();

            return await GenerateCertificationFulfillmentStatuses(employeeCertifications);
        }

        private async Task<List<CertificationFulfillmentStatus>> GenerateCertificationFulfillmentStatuses(List<EmployeeCertification> employeeCertifications)
        {
            Dictionary<int, CertificationCalculatorFulfillmentType> employeeCertsWithCalculator = new Dictionary<int, CertificationCalculatorFulfillmentType>();

            foreach (var employeeCertification in employeeCertifications)
            {
                var calcType = _certificationFulfillmentCalculatorFactory.GetCertificationCalculatorFulfillmentType(employeeCertification);
                employeeCertsWithCalculator.Add(employeeCertification.Id, calcType);
            }

            List<CertificationFulfillmentStatus> certificationFulfillmentStatuses = new List<CertificationFulfillmentStatus>();

            foreach (var calculatorType in employeeCertsWithCalculator.Values.Distinct())
            {
                var employeeCertificationsOfType = employeeCertsWithCalculator.Where(r => r.Value == calculatorType);
                var calculator = _certificationFulfillmentCalculatorFactory.CreateCertificationFulfillmentCalculator(calculatorType);

                certificationFulfillmentStatuses.AddRange(await calculator.GetFulfillmentStatusesAsync(employeeCertificationsOfType.Select(r => r.Key).ToList()));
            }

            return certificationFulfillmentStatuses;
        }
    }
}
