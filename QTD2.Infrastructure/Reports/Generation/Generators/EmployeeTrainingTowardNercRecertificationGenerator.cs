using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Certifications;
using QTD2.Domain.Certifications.Factories.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
   public class EmployeeTrainingTowardNercRecertificationGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ICertificationService _certificationService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;


        public EmployeeTrainingTowardNercRecertificationGenerator(
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
            string templatePath = "EmployeeTrainingTowardNercRecertification.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            List<int> employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
            var includeScheduledILAs = ExtractParameters<bool>(report.Filters, "INCLUDE SCHEDULED ILAS");
            var includeNERCProviderILAsOnly = ExtractParameters<bool>(report.Filters, "INCLUDE NERC PROVIDER ILAS ONLY");
            var includeFailedGrade = ExtractParameters<bool>(report.Filters, "INCLUDE FAILED GRADE");
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var nercCertifications = await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC");
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
            var nercCertFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesAsync(employeeIds, nercCertifications.Select(c => c.Id).ToList());

            if (!includeScheduledILAs)
            {
				foreach (var nercCertFulfillmentStatus in nercCertFulfillmentStatuses)
				{
                    nercCertFulfillmentStatus.FulfillmentRecords = nercCertFulfillmentStatus.FulfillmentRecords.Where(f => f.IsComplete).ToList();
				}
            }

            if (includeNERCProviderILAsOnly)
            {
                foreach (var nercCertFulfillmentStatus in nercCertFulfillmentStatuses)
                {
                    nercCertFulfillmentStatus.FulfillmentRecords = nercCertFulfillmentStatus.FulfillmentRecords.Where(f => f.ILAProviderIsNERC).ToList();
                }
            }

            if (!includeFailedGrade)
            {
                foreach (var nercCertFulfillmentStatus in nercCertFulfillmentStatuses)
                {
                    nercCertFulfillmentStatus.FulfillmentRecords = nercCertFulfillmentStatus.FulfillmentRecords.Where(x => x.Grade != "F").ToList();
                }
            }

            return new EmployeeTrainingTowardNercRecertification(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, nercCertFulfillmentStatuses);
        }
    }
}
