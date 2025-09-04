using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class ProceduresByEnablingObjectivesGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IEnablingObjectiveService _enablingObjectiveService;
        private readonly IProcedureService _procedureService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public ProceduresByEnablingObjectivesGenerator(
            IClientUserSettings_GeneralSettingService generalSettingService,
            IEnablingObjectiveService enablingObjectiveService,
            IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
            IProcedureService procedureService
            )
        {
            _generalSettingService = generalSettingService;
            _enablingObjectiveService = enablingObjectiveService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _procedureService = procedureService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "ProceduresByEnablingObjectives.cshtml";
            var companyLogo = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var defaultTimeZone = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();

            var enablingObjectiveIds = ExtractParameters<List<int>>(report.Filters, "SELECT ENABLING OBJECTIVE");
            var onlyShowEosWithProcedureLinked = ExtractParameters<bool>(report.Filters, "ONLY SHOW EOS WITH PROCEDURE LINKED");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var enablingObjectives = await _enablingObjectiveService.GetProcedureLinkedEnablingObjectivesAsync(enablingObjectiveIds);
            var distinctProcedureIds = enablingObjectives.SelectMany(eo => eo.Procedure_EnablingObjective_Links).Select(link => link.ProcedureId).Distinct().ToList();
            var distinctProcedureMetaIds = enablingObjectives.SelectMany(x => x.EnablingObjective_MetaEO_Links).SelectMany(x => x.EnablingObjective.Procedure_EnablingObjective_Links.Select(x => x.ProcedureId)).Distinct().ToList();
            var procedures = await _procedureService.GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(distinctProcedureIds);
            var metaProcedures = await _procedureService.GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(distinctProcedureMetaIds);
            var procedureDict = procedures.ToDictionary(p => p.Id);
            var metaProcedureDict = metaProcedures.ToDictionary(p => p.Id);


            if (onlyShowEosWithProcedureLinked)
            {
                enablingObjectives = enablingObjectives.Where(enablingObjective => enablingObjective.Procedure_EnablingObjective_Links.Any(r => r.Active)).ToList();
            }

            foreach (var enablingObjective in enablingObjectives)
            {
                foreach (var link in enablingObjective.Procedure_EnablingObjective_Links)
                {
                    if (procedureDict.TryGetValue(link.ProcedureId, out var matchedProcedure))
                    {
                        link.Procedure = matchedProcedure;
                    }
                }
                if (onlyShowEosWithProcedureLinked)
                {
                    enablingObjective.EnablingObjective_MetaEO_Links = enablingObjective.EnablingObjective_MetaEO_Links.Where(x => x.EnablingObjective.Procedure_EnablingObjective_Links.Count() > 0).ToList();
                }
                foreach (var metaLink in enablingObjective.EnablingObjective_MetaEO_Links)
                {
                    var metaEO = metaLink.EnablingObjective;
                    foreach (var link in metaEO.Procedure_EnablingObjective_Links)
                    {
                        if (metaProcedureDict.TryGetValue(link.ProcedureId, out var matchedMetaProcedure))
                        {
                            link.Procedure = matchedMetaProcedure;
                        }
                    }
                }
            }

            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new ProceduresByEnablingObjectivesModel(report.InternalReportTitle, templatePath, displayColumns, companyLogo, enablingObjectives.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}