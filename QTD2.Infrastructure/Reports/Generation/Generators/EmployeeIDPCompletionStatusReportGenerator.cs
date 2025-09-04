using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class EmployeeIDPCompletionStatusReportGenerator : ReportModelGenerator
    {
        private readonly ICertificationService _certificationService;
        private readonly ICertificationReportHelper _certificationReportHelper;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly IIDPScheduleService _iDPScheduleService;

        public EmployeeIDPCompletionStatusReportGenerator(
            ICertificationService certificationService,
            ICertificationReportHelper certificationReportHelper,
            ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory,
            IClientUserSettings_GeneralSettingService generalSettingService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IIDPScheduleService iDPScheduleService
          )
        {
            _certificationService = certificationService;
            _certificationReportHelper = certificationReportHelper;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _iDPScheduleService = iDPScheduleService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "EmployeeIDPCompletionStatus.cshtml";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var companyLogo = "";
            var defaultTimeZone = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var employeeIds = ExtractParameters<List<int>>(report.Filters, "SELECT EMPLOYEES");
            var year = ExtractParameters<int>(report.Filters, "SELECT YEAR");
            var activeInactiveAllILAs = ExtractParameters<String>(report.Filters, "ACTIVE/INACTIVE/ALL ILAS");
            var ilaCompletionStatus = ExtractParameters<String>(report.Filters, "ILA COMPLETION STATUS");

            var nercCertificationIds = (await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC")).Select(c => c.Id).ToList();
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
            var certificationFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesForYearAsync(
                employeeIds, 
                nercCertificationIds, 
                year);

            // Get EmergencyResponse /  Basic Certification information
            List<string> additionalCertificationAcronyms = new List<string> { "Emergency Response", "Reg", "Reg2", "Other" };
            var additionalCertifications = await _certificationService.FindAsync(c => additionalCertificationAcronyms.Contains(c.InternalIdentifier));

            var certification_EmergencyResponse = additionalCertifications.Where(c => c.InternalIdentifier == "Emergency Response").FirstOrDefault();

            var certificationId_Reg = additionalCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault();
            var certificationId_Reg2 = additionalCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault();
            var certificationId_Other = additionalCertifications.Where(c => c.InternalIdentifier == "Other").Select(c => c.Id).FirstOrDefault();

            var emergencyResponseCertCalculator = _certificationFulfillmentCalculatorFactory.CreateEmergencyResponseCalculator();
            var basicCertCalculator = _certificationFulfillmentCalculatorFactory.CreateBasicCalculator();

            var emergencyResponseFulfillmentStatuses = await emergencyResponseCertCalculator.GetFulfillmentStatusesForYearAsync(
                employeeIds,
                new List<int>() { certification_EmergencyResponse.Id },
                year);

            var regCertificationFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesForYearAsync(
                employeeIds,
                new List<int>() { certificationId_Reg },
                year);

            var reg2CertificationFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesForYearAsync(
                employeeIds,
                new List<int>() { certificationId_Reg2 },
                year);

            var otherCertificationFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesForYearAsync(
                employeeIds,
                new List<int>() {  certificationId_Other },
                year);

            certificationFulfillmentStatuses.AddRange(emergencyResponseFulfillmentStatuses);
            certificationFulfillmentStatuses.AddRange(regCertificationFulfillmentStatuses);
            certificationFulfillmentStatuses.AddRange(reg2CertificationFulfillmentStatuses);
            certificationFulfillmentStatuses.AddRange(otherCertificationFulfillmentStatuses);

            // Clean up (this might be useless although I don't know the reason it was added so I'll leave it)
            certificationFulfillmentStatuses = certificationFulfillmentStatuses.Where(r => r != null).ToList();
            var idpSchedules = await _iDPScheduleService.GetIDPSchedulesForEmployeeIDPCompletionStatusReportFulfillments(year, employeeIds);

            if (activeInactiveAllILAs == "Active Only")
            {
                foreach (var certificationFulfillmentStatus in certificationFulfillmentStatuses)
                {
                    certificationFulfillmentStatus.FulfillmentRecords = certificationFulfillmentStatus.FulfillmentRecords.Where(fr => fr.ILAActive).ToList();
                }
                idpSchedules = idpSchedules.Where(r => r.IDP.ILA.Active).ToList();
            }
            else if (activeInactiveAllILAs == "Inactive Only")
            {
                foreach (var certificationFulfillmentStatus in certificationFulfillmentStatuses)
                {
                    certificationFulfillmentStatus.FulfillmentRecords = certificationFulfillmentStatus.FulfillmentRecords.Where(fr => !fr.ILAActive).ToList();
                }
                idpSchedules = idpSchedules.Where(r => !r.IDP.ILA.Active).ToList();
            }

            if (ilaCompletionStatus == "Completed")
            {
                foreach (var certificationFulfillmentStatus in certificationFulfillmentStatuses)
                {
                    certificationFulfillmentStatus.FulfillmentRecords = certificationFulfillmentStatus.FulfillmentRecords.Where(fr => fr.IsComplete).ToList();
                }
                idpSchedules = idpSchedules.Where(r => r.CompletionDate != null).ToList();
            }
            else if (ilaCompletionStatus == "Not Completed")
            {
                foreach (var certificationFulfillmentStatus in certificationFulfillmentStatuses)
                {
                    certificationFulfillmentStatus.FulfillmentRecords = certificationFulfillmentStatus.FulfillmentRecords.Where(fr => !fr.IsComplete).ToList();
                }
                idpSchedules = idpSchedules.Where(r => r.CompletionDate == null).ToList();
            }
           
            return new EmployeeIDPCompletionStatusReport(report.InternalReportTitle, templatePath, displayColumns, companyLogo, certificationFulfillmentStatuses, certification_EmergencyResponse, certificationId_Reg, certificationId_Reg2, certificationId_Other, idpSchedules, defaultTimeZone, labelReplacement,year, ilaCompletionStatus);
        }
    }
}
