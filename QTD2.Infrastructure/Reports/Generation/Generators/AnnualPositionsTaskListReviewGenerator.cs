using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Helpers;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;

namespace QTD2.Infrastructure.Reports.Generation.Generators
{
    public class AnnualPositionsTaskListReviewGenerator : ReportModelGenerator
    {
        private readonly IClientUserSettings_GeneralSettingService _generalSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientSettings_LabelReplacementsService;
        private readonly ITaskListReviewService _taskListReviewService;
        private readonly ITaskReviewService _taskReviewService;

        public AnnualPositionsTaskListReviewGenerator(
          IClientUserSettings_GeneralSettingService generalSettingService,
          IClientSettings_LabelReplacementsService clientSettings_LabelReplacementsService,
          ITaskListReviewService taskListReviewService,
           ITaskReviewService taskReviewService
          )
        {
            _generalSettingService = generalSettingService;
            _clientSettings_LabelReplacementsService = clientSettings_LabelReplacementsService;
            _taskListReviewService = taskListReviewService;
            _taskReviewService = taskReviewService;
        }
        public override async Task<IReportModel> GenerateModel(Report report)
        {
            string templatePath = "AnnualPositionsTaskListReview.cshtml";
            var companyLogo = "";
            var defaultTimeZone = "";
            var defaultDateFormat = "";
            var generalSettings = await _generalSettingService.GetGeneralSettings();
            List<string> displayColumns = report.DisplayColumns.Where(r => r.Display).Select(r => r.ColumnName).ToList();
            var taskListReviewfilter = report.Filters.FirstOrDefault(x => x.Name == "Task List Reviews" && !String.IsNullOrEmpty(x.Value));
            var taskListReviewIds = taskListReviewfilter != null ? ExtractParameters<List<int>>(report.Filters, "TASK LIST REVIEWS") : new List<int>();
            var activeStatus = ExtractParameters<string>(report.Filters, "STATUS");
            var labelReplacement = await _clientSettings_LabelReplacementsService.GetLabelReplacementAsync();

            var taskListReviews = await _taskListReviewService.GetTaskListReviewsByIdsAndStatusAsync(taskListReviewIds, activeStatus);
            foreach(var taskListReview in taskListReviews)
            {
                List<int> taskReviewIds = taskListReview.TaskReviews.Select(x => x.Id).ToList();
                taskListReview.TaskReviews = (await _taskReviewService.GetTaskReviewsByIdsAsync(taskReviewIds)).OrderBy(t => t.Task?.FullNumber, new AlphaNumericSortHelper()).ToList();
                foreach(var taskReview in taskListReview.TaskReviews)
                {
                    taskReview.Task.Version_Tasks = taskReview.Task.Version_Tasks.OrderByDescending(t => t.VersionNumber, new AlphaNumericSortHelper()).ToList();
                }
            }
            if (generalSettings != null)
            {
                companyLogo = generalSettings.CompanyLogo;
                defaultTimeZone = generalSettings.DefaultTimeZone;
                defaultDateFormat = generalSettings.DateFormat;
            }
            return new AnnualPositionsTaskListReview(report.InternalReportTitle, templatePath, displayColumns, companyLogo, taskListReviews, labelReplacement, defaultTimeZone, defaultDateFormat);
        }
    }
}
