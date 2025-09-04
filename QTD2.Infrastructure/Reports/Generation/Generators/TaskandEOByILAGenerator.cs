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
   public class TaskandEOByILAGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly ITaskService _taskService;
        private readonly IILAService _iLAService;
        private readonly IILA_EnablingObjective_LinkService _iLA_EnablingObjective_LinkService;
        private readonly IILA_TaskObjective_LinkService _iLA_TaskObjective_LinkService;
        private readonly IILACustomObjective_LinkService _iLACustomObjective_LinkService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;

        public TaskandEOByILAGenerator(
        IClientUserSettings_GeneralSettingService generalSettingService,
        ITaskService taskService,
        IILAService iLAService,
        IILA_EnablingObjective_LinkService iLA_EnablingObjective_LinkService,
        IILA_TaskObjective_LinkService iLA_TaskObjective_LinkService,
        IILACustomObjective_LinkService iLACustomObjective_LinkService,
        IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService
        )
        {
            _generalSettingService = generalSettingService;
            _taskService = taskService;
            _iLAService = iLAService;
            _iLA_EnablingObjective_LinkService = iLA_EnablingObjective_LinkService;
            _iLA_TaskObjective_LinkService = iLA_TaskObjective_LinkService;
            _iLACustomObjective_LinkService = iLACustomObjective_LinkService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
        }

        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "TaskandEOByILA.cshtml";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            var companyLogo = "";
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var defaultTimeZone = "";
            var iLAIds = ExtractParameters<List<int>>(report.Filters, "ILA");
            var taskEoFilter = ExtractParameters<List<int>>(report.Filters, "INCLUDE TASK AND EO FILTER");
            var taskInclude = taskEoFilter.Contains(1);
            var includeMetaTask = taskEoFilter.Contains(2);
            var eoInclude = taskEoFilter.Contains(3);
            var sqInclude= taskEoFilter.Contains(4);
            var includeMetaEo = taskEoFilter.Contains(5);
            var customEoInclude = taskEoFilter.Contains(6);

            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();
            var iLAs = (await _iLAService.GetILAsWithProvider(iLAIds)).OrderBy(m => m.Number).ToList();
            foreach (var ila in iLAs)
            {
                if (taskInclude || includeMetaTask)
                {
                    var taskObjectiveLinks = await _iLA_TaskObjective_LinkService.GetILA_TaskObjective_LinksByILAId(ila.Id);
                    var filteredLinks = new List<ILA_TaskObjective_Link>();

                    if (taskInclude)
                    {
                        filteredLinks.AddRange(taskObjectiveLinks.Where(link => link.Task != null && link.Task.Active && !link.Task.IsMeta));
                    }

                    if (includeMetaTask)
                    {
                        filteredLinks.AddRange(taskObjectiveLinks.Where(link => link.Task != null && link.Task.Active && link.Task.IsMeta));
                    }

                    if (filteredLinks.Any())
                    {
                        ila.ILA_TaskObjective_Links = filteredLinks;
                    }
                }

                if (eoInclude || sqInclude || includeMetaEo)
                {
                    var enablingObjectiveLinks = await _iLA_EnablingObjective_LinkService.GetILA_EnablingObjective_LinksByILAId(ila.Id);
                    var filteredLinks = new List<ILA_EnablingObjective_Link>();

                    if (eoInclude)
                    {
                        filteredLinks.AddRange(enablingObjectiveLinks.Where(link => link.EnablingObjective != null && link.EnablingObjective.Active && !link.EnablingObjective.IsSkillQualification && !link.EnablingObjective.isMetaEO));
                    }

                    if (sqInclude)
                    {
                        filteredLinks.AddRange(enablingObjectiveLinks.Where(link => link.EnablingObjective != null && link.EnablingObjective.Active && link.EnablingObjective.IsSkillQualification && !link.EnablingObjective.isMetaEO));
                    }

                    if (includeMetaEo)
                    {
                        filteredLinks.AddRange(enablingObjectiveLinks.Where(link => link.EnablingObjective != null && link.EnablingObjective.Active && link.EnablingObjective.isMetaEO));
                    }

                    if (filteredLinks.Any())
                    {
                        ila.ILA_EnablingObjective_Links = filteredLinks;
                    }
                }

                if (customEoInclude)
                {
                    var customObjectiveLinks = await _iLACustomObjective_LinkService.GetILA_CustomObjective_LinksByILAId(ila.Id);
                    var filteredLinks = customObjectiveLinks.Where(link => link.CustomEnablingObjective != null && link.CustomEnablingObjective.Active).ToList();

                    if (filteredLinks.Any())
                    {
                        ila.ILACustomObjective_Links = filteredLinks;
                    }
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
            }

            return new TaskandEOByILA(report.InternalReportTitle, templatePath, displayColumns, companyLogo,iLAs.ToList(), labelReplacement, defaultTimeZone);
        }
    }
}
