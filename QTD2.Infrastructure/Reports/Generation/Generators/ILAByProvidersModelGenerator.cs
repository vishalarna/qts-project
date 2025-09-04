using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
     public class ILAByProvidersModelGenerator : ReportModelGenerator
     {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IILAService _ilaService;
        private readonly IProviderService _providersService;
        private ICertificationService _certificationService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ILAByProvidersModelGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        IILAService ilaService,
        IProviderService providersService,
        ICertificationService certificationService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _ilaService = ilaService;
            _providersService = providersService;
            _certificationService = certificationService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ILAByProvider.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            //build data;
            var selectedProviders = ExtractParameters<List<int>>(report.Filters, "PROVIDERS");
            var ilaStatus = ExtractParameters<string>(report.Filters, "ILA STATUS");
            var showIlaAppDates = ExtractParameters<bool>(report.Filters, "SHOW ILA APPLICATION DATES");
            //TODO pass provider list int filter into this 
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var providers = await _providersService.GetByProvidersListAsync(selectedProviders);
            var ilas = await _ilaService.GetByProvidersListAsync(selectedProviders, ilaStatus);
            var certifications = await _certificationService.GetByListOfIDsWithSubRequirementsAsync(ilas.SelectMany(r => r.ILACertificationLinks.Select(s => s.CertificationId).Distinct()).ToList());

            foreach (var ila in ilas)
            {
                foreach (var certLink in ila.ILACertificationLinks)
                {
                    certLink.Certification = certifications.Where(r => r.Id == certLink.CertificationId).First();
                }
            }

            foreach (var provider in providers)
            {
                provider.ILAs = ilas.Where(r => r.ProviderId == provider.Id).ToList();
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }
            return new ILAByProviders(report.InternalReportTitle, templatePath, displayColumns, providers.ToList(), companyLogo, labelReplacement, defaultTimeZone,showIlaAppDates);
        }

    }
}
