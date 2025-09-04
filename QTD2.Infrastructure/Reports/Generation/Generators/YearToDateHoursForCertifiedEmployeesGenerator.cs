using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
	public class YearToDateHoursForCertifiedEmployeesGenerator : ReportModelGenerator
	{
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
		private readonly ICertificationService _certificationService;
		private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;

		public YearToDateHoursForCertifiedEmployeesGenerator(
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
			string templatePath = "YearToDateHoursForCertifiedEmployees.cshtml";
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
			var year = ExtractParameters<int>(report.Filters, "SELECT YEAR");

			var nercCertifications = await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC");
			var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
			var nerCertFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesAsync(employeeIds, nercCertifications.Select(c => c.Id).ToList());

			List<string> additionalCertificationDescriptions = new List<string> { "Reg", "Reg2", "Other" };
			var additionalCertifications = await _certificationService.FindAsync(c => additionalCertificationDescriptions.Contains(c.InternalIdentifier));
			var basicCertCalculator = _certificationFulfillmentCalculatorFactory.CreateBasicCalculator();
			var basicCertFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesAsync(employeeIds, additionalCertifications.Select(c => c.Id).ToList());

			//Limit Reg, Reg2, and Other CertificationFulfillmentStatuses to be only one of each if multiple exist
			var firstReg = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
			var firstReg2 = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();
			var firstOther = basicCertFulfillmentStatuses.GroupBy(bc => bc.EmployeeId).Select(g => g.OrderBy(c => c.IssueDate).FirstOrDefault(c => c.CertificationId == additionalCertifications.Where(c => c.InternalIdentifier == "Other").Select(c => c.Id).FirstOrDefault())).Where(c => c != null).ToList();

			nerCertFulfillmentStatuses.AddRange(firstReg);
			nerCertFulfillmentStatuses.AddRange(firstReg2);
			nerCertFulfillmentStatuses.AddRange(firstOther);

			var certificationFulfillmentStatuses = nerCertFulfillmentStatuses.ToList();			
			return new YearToDateHoursForCertifiedEmployees(report.InternalReportTitle, templatePath, displayColumns, companyLogo, defaultTimeZone, labelReplacement, certificationFulfillmentStatuses, nercCertifications.Select(c => c.Id).ToList(), additionalCertifications.Where(c => c.InternalIdentifier == "Reg").Select(c => c.Id).FirstOrDefault(), additionalCertifications.Where(c => c.InternalIdentifier == "Reg2").Select(c => c.Id).FirstOrDefault(), additionalCertifications.Where(c => c.InternalIdentifier == "Other").Select(c => c.Id).FirstOrDefault(), year);
		}
	}
}
