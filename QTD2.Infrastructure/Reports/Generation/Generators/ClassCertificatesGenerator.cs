using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using QTD2.Domain.Certifications.Factories.Implimentations;
using QTD2.Domain.Certifications.Factories.Interfaces;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Helpers.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class ClassCertificatesGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ICertificationService _certificationService;
        private readonly ICertificationReportHelper _certificationReportHelper;
        private readonly IImageProvider _imageProvider;
        private readonly IILAService _iLAService;
        private readonly ICertificationFulfillmentCalculatorFactory _certificationFulfillmentCalculatorFactory;

        public ClassCertificatesGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClassScheduleEmployeeService classScheduleEmployeeService,
          ICertificationService certificationService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ICertificationReportHelper certificationReportHelper, 
          IImageProvider imageProvider,
          IILAService iLAService,
          ICertificationFulfillmentCalculatorFactory certificationFulfillmentCalculatorFactory
          )
        {
            _generalSettingService = generalSettingService;
            _certificationService = certificationService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _certificationReportHelper = certificationReportHelper;
            _imageProvider = imageProvider;
            _iLAService = iLAService;
            _certificationFulfillmentCalculatorFactory = certificationFulfillmentCalculatorFactory;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ClassCertificates.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var dateFormat = "";
            var classScheduleIds = ExtractParameters<List<int>>(report.Filters, "SELECT CLASS");
            var printForThoseWithNoGradeAwarded = ExtractParameters<bool>(report.Filters, "PRINT FOR ALL REGISTERED STUDENTS BEFORE GRADE IS AWARDED");
            var includeFailedStudents = ExtractParameters<bool>(report.Filters, "INCLUDE FAILED STUDENTS");

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var nercLogo = _imageProvider.GetNERCLogo();

            var classScheduleEmployees = await _classScheduleEmployeeService.GetClassCertificatesAsync(classScheduleIds, printForThoseWithNoGradeAwarded, includeFailedStudents);
            var distinctIlaIds = classScheduleEmployees.Where(x=>x.ClassSchedule.ILAID !=null).Select(m => m.ClassSchedule.ILAID.Value).Distinct().ToList();
            var ilas = await _iLAService.GetILAsWithCertificationLinksOnlyAsync(distinctIlaIds);
           
            var employeeIds = classScheduleEmployees.Select(cse => cse.EmployeeId).Distinct().ToList();

            var nercCertificationIds = (await _certificationService.FindAsync(c => c.CertifyingBody.Name == "NERC")).Select(c => c.Id).ToList();
            var nercCertCalculator = _certificationFulfillmentCalculatorFactory.CreateNercCalculator();
            var certificationFulfillmentStatuses = await nercCertCalculator.GetFulfillmentStatusesAsync(
                employeeIds,
                nercCertificationIds);

            List<string> emergencyResponseCertificationDescriptions = new List<string> { "Emergency Response" };
            var emergencyResponseCertificationId = (await _certificationService.FindAsync(c => emergencyResponseCertificationDescriptions.Contains(c.InternalIdentifier))).Select(c => c.Id).FirstOrDefault();
            var emergencyResponseCertCalculator = _certificationFulfillmentCalculatorFactory.CreateEmergencyResponseCalculator();
            var emergencyResponseCertificationFulfillmentStatuses = await emergencyResponseCertCalculator.GetFulfillmentStatusesAsync(
                employeeIds,
                new List<int>() { emergencyResponseCertificationId });

            var profHoursCertificationId = (await _certificationService.FindAsync(c => c.Name == "Professional")).FirstOrDefault().Id;
            var basicCertCalculator = _certificationFulfillmentCalculatorFactory.CreateBasicCalculator();
            var basicCertFulfillmentStatuses = await basicCertCalculator.GetFulfillmentStatusesAsync(employeeIds, new List<int>() { profHoursCertificationId });

            certificationFulfillmentStatuses.AddRange(emergencyResponseCertificationFulfillmentStatuses);

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                dateFormat = generalSettings.DateFormat;
            }

            return new ClassCertificatesModel(
                report.InternalReportTitle,
                templatePath,
                displayColumns,
                companyLogo,
                labelReplacement,
                defaultTimeZone,
                classScheduleEmployees,
                certificationFulfillmentStatuses,
                emergencyResponseCertificationId,
                profHoursCertificationId,
                printForThoseWithNoGradeAwarded, 
                nercLogo,
                ilas, dateFormat);
        }
    }
}
