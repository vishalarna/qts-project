using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class TrainingSummaryByPositionGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IPositionService _positionService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ICertificationService _certificationService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;
        public TrainingSummaryByPositionGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
            IPositionService positionService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            ICertificationService certificationService,
            ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory
          )
        {
            _generalSettingService = generalSettingService;
            _positionService = positionService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _certificationService = certificationService;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TrainingSummaryByPosition.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var positionIds = ExtractParameters<List<int>>(report.Filters, "POSITION");
            var organizationFilter = report.Filters.FirstOrDefault(x => x.Name == "Organization" && !string.IsNullOrEmpty(x.Value));
            var organizationIDs = organizationFilter != null ? ExtractParameters<List<int>>(report.Filters, "ORGANIZATION") : new List<int>();
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var positions = await _positionService.GetAllTrainingSummaryByPositionAsync(positionIds, organizationIDs);
            var employeeIds = positions.SelectMany(p => p.EmployeePositions).Select(ep => ep.Employee.Id).Distinct().ToList();

            var nercCertifications = await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC");
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
            var nerCertFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesAsync(employeeIds, nercCertifications.Select(c => c.Id).ToList());

            List<string> basicCertificationDescriptions = new List<string> { "Reg", "Reg2", "Other" };
            var profHoursCertification = (await _certificationService.FindAsync(c => c.Name == "Professional")).FirstOrDefault();
            var basicCertifications = (await _certificationService.FindAsync(c => basicCertificationDescriptions.Contains(c.InternalIdentifier))).ToList();
            basicCertifications.Add(profHoursCertification); 
            var basicCertCalculator = _certificationFulfillmentCalculatorFactory.CreateBasicCalculator();
            var basicCertFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesAsync(employeeIds, basicCertifications.Select(c => c.Id).ToList());

            List<string> emergencyResponseCertificationDescriptions = new List<string> { "Emergency Response" };
            var emergencyResponseCertifications = await _certificationService.FindAsync(c => emergencyResponseCertificationDescriptions.Contains(c.InternalIdentifier));
            var emergencyResponseCertCalculator = _certificationFulfillmentCalculatorFactory.CreateEmergencyResponseCalculator();
            var emergencyResponseCertFulfillmentStatuses = await emergencyResponseCertCalculator.GetFulfillmentStatusesWhenRelatedEmployeeCertificationExistsAsync(employeeIds, emergencyResponseCertifications.Select(c => c.Id).ToList(), nercCertifications.Select(c => c.Id).ToList());

            //Limit Reg, Reg2, and Other CertificationFulfillmentStatuses to be only one of each if multiple exist
            var firstReg = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == basicCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            var firstReg2 = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == basicCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            var firstOther = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == basicCertifications.Where(c => c.InternalIdentifier == "Other").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
            var profHour = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == profHoursCertification.Id)).Where(c => c != null).ToList();

            nerCertFulfillmentStatuses.AddRange(firstReg);
            nerCertFulfillmentStatuses.AddRange(firstReg2);
            nerCertFulfillmentStatuses.AddRange(firstOther);
            nerCertFulfillmentStatuses.AddRange(profHour);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            var certificationstatus = nerCertFulfillmentStatuses.Where(r => r != null).ToList();
            return new TrainingSummaryByPosition(report.InternalReportTitle, templatePath, displayColumns, companyLogo, positions.ToList(), organizationFilter != null, labelReplacement, defaultTimeZone, certificationstatus, emergencyResponseCertFulfillmentStatuses, basicCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault(), basicCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault(), basicCertifications.Where(c => c.InternalIdentifier == "Other").Select(c => c.Id).FirstOrDefault(), profHoursCertification.Id);

        }
    }
}
