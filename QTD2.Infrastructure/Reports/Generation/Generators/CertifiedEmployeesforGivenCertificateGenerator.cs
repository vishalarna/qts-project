using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;


namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class CertifiedEmployeesforGivenCertificateGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ICertificationService _certificationService;
        private readonly IEmployeeService _employeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IOrganizationService _organizationService;

        public CertifiedEmployeesforGivenCertificateGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            ICertificationService certificationService,
            IEmployeeService employeeService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
        IOrganizationService organizationService
        )
        {
            _generalSettingService = generalSettingService;
            _certificationService = certificationService;
            _employeeService = employeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _organizationService = organizationService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "CertifiedEmployeesforGivenCertificate.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var certificationIds = ExtractParameters<List<int>>(report.Filters, "SELECT CERTIFICATE");
            var organizationfilter = report.Filters.FirstOrDefault(x => x.Name == "Filter by Organization" && !String.IsNullOrEmpty(x.Value));
            var organizationIds = organizationfilter != null ? ExtractParameters<List<int>>(report.Filters, "FILTER BY ORGANIZATION") : new List<int>();
            var companyEmployeeStatus = ExtractParameters<string>(report.Filters, "ALL COMPANY EMPLOYEES");
            var organizations = await _organizationService.GetByIdListAsync(organizationIds);
            var certifications = await _certificationService.GetCertificationsByIdAsync(certificationIds);
            var employeeIDs = certifications.SelectMany(c => c.EmployeeCertifications).Select(ec => ec.EmployeeId).Distinct().ToList();
            var employees = await _employeeService.GetCertifiedEmployeesforGivenCertificateAsync(employeeIDs, companyEmployeeStatus);

            foreach (var certification in certifications)
            {
                foreach (var employeeCertification in certification.EmployeeCertifications)
                {
                    var matchingEmployee = employees.FirstOrDefault(e => e.Id == employeeCertification.EmployeeId);

                    if (matchingEmployee != null)
                    {
                        employeeCertification.Employee = matchingEmployee;
                        employeeCertification.Employee.EmployeeOrganizations = employeeCertification.Employee.EmployeeOrganizations.Where(x => organizationIds.Contains(x.OrganizationId)).ToList();
                    }
                }
                if (organizationIds.Count() > 0)
                {
                    certification.EmployeeCertifications = certification.EmployeeCertifications.Where(x => x.Employee != null && x.Employee.EmployeeOrganizations.Count() > 0).ToList();
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new CertifiedEmployeesforGivenCertificate(report.InternalReportTitle, templatePath, displayColumns, companyLogo, certifications.ToList(), labelReplacement, defaultTimeZone, organizations.ToList());
        }
    }
}
