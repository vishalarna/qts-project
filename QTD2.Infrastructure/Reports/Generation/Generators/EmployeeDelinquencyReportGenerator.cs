using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Certifications.Factories.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class EmployeeDelinquencyReportGenerator : ReportModelGenerator
    {
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ICertificationService _certificationService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;

        public EmployeeDelinquencyReportGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ICertificationService certificationService,
            ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory
       )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _certificationService = certificationService;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeDelinquency.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var companyLogo = "";
            var defaultTimeZone = "";
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
            var sortEmployeesByOrg = ExtractParameters<bool>(report.Filters, "Sort Employees by Organization");

            var nercCertifications = await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC");
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
            var nercCertFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesAsync(employeeIds, nercCertifications.Select(c => c.Id).ToList());

            // Limit to only the most recently issued EmployeeCertification per Certification type
            nercCertFulfillmentStatuses = nercCertFulfillmentStatuses.GroupBy(c => new { c.EmployeeId, c.CertificationId }).Select(g => g.OrderByDescending(ec => ec.IssueDate).First()).ToList();

            List<string> additionalCertificationDescriptions = new List<string> { "Emergency Response", "Reg", "Reg2" };
            var additionalCertifications = await _certificationService.FindAsync(c => additionalCertificationDescriptions.Contains(c.InternalIdentifier));
            var basicCertCalculator = _certificationFulfillmentCalculatorFactory.CreateBasicCalculator();
            var basicCertFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesAsync(employeeIds, additionalCertifications.Select(c => c.Id).ToList());

            //Limit Emergency Response, Reg and Reg2 CertificationFulfillmentStatuses to be only one of each if multiple ex+ist
            var firstEmergencyResponse = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderByDescending(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Emergency Response").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            var firstReg = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            var firstReg2 = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            nercCertFulfillmentStatuses.AddRange(firstEmergencyResponse);

            nercCertFulfillmentStatuses.AddRange(firstReg);
            nercCertFulfillmentStatuses.AddRange(firstReg2);

            var certificationFulfillmentStatuses = nercCertFulfillmentStatuses.Where(r => r != null).OrderBy(cfs => cfs.EmployeeLastName).ToList();
            var organizations = certificationFulfillmentStatuses.SelectMany(cfs => cfs.Employee.EmployeeOrganizations).Select(org => org.Organization).Distinct().ToList();

            return new EmployeeDelinquencyReport(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, certificationFulfillmentStatuses, organizations, nercCertifications.Select(c => c.Id).ToList(), additionalCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault(), additionalCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault(), sortEmployeesByOrg, additionalCertifications.Where(c => c.InternalIdentifier == "Emergency Response").Select(c => c.Id).FirstOrDefault());
        }
    }
}
