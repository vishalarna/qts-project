using DocumentFormat.OpenXml.Spreadsheet;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingIssue;
using QTD2.Infrastructure.Model.TrainingProgram;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITrainingProgramReview_TrainingIssue_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ITrainingProgramReview_TrainingIssue_LinkService;

namespace QTD2.Application.Services.Shared
{
    public class TrainingProgramReview_TrainingIssue_LinkService: ITrainingProgramReview_TrainingIssue_LinkService
    {
        private readonly ITrainingProgramReview_TrainingIssue_LinkDomainService _trainingProgramReview_TrainingIssue_LinkDomainService;
        public TrainingProgramReview_TrainingIssue_LinkService(ITrainingProgramReview_TrainingIssue_LinkDomainService trainingProgramReview_TrainingIssue_LinkDomainService)
        {
            _trainingProgramReview_TrainingIssue_LinkDomainService = trainingProgramReview_TrainingIssue_LinkDomainService;
        }
        public async Task<List<TrainingIssue_VM>> GetTrainingProgramTrainingIssuesLinkById(int id)
        {
            var links = await _trainingProgramReview_TrainingIssue_LinkDomainService.GetTrainingProgramTrainingIssueLinkByTrainingProgramId(id);
            return links.Select(link => new TrainingIssue_VM(link.TrainingIssueId, link.TrainingIssue?.IssueID, link.TrainingIssue?.IssueTitle, link.TrainingIssue.TrainingIssueCreatedDate, link.TrainingIssue.DueDate,link.TrainingIssue.Status.Status,link.TrainingIssue.Severity.Severity,link.TrainingIssue.TaskReviewId)).ToList();
        }

        public async System.Threading.Tasks.Task CreateTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options)
        {
            var existingLinkIds = (await _trainingProgramReview_TrainingIssue_LinkDomainService.GetTrainingProgramTrainingIssueLinkByTrainingProgramId(id)).Select(m=>m.Id).ToList();
            var newIssueIds = options.TrainingIssueIds.Except(existingLinkIds).ToList()?? new List<int>();
            foreach (var issueId in newIssueIds)
            {
                var newLink = new TrainingProgramReview_TrainingIssue_Link
                {
                    TrainingProgramId = id,
                    TrainingIssueId = issueId
                };
                await _trainingProgramReview_TrainingIssue_LinkDomainService.AddAsync(newLink);
            }
        }

        public async System.Threading.Tasks.Task RemoveTrainingProgramTrainingIssueLinks(int id, TrainingProgramTrainingIssueLinkOption options)
        {
            var existingLinks = (await _trainingProgramReview_TrainingIssue_LinkDomainService.GetTrainingProgramTrainingIssueLinkByTrainingProgramId(id)).ToList();
            var linksToRemove = existingLinks.Where(x=>options.TrainingIssueIds.Contains(x.TrainingIssueId)).ToList();
            linksToRemove.ForEach(x => x.Delete());
            await _trainingProgramReview_TrainingIssue_LinkDomainService.BulkUpdateAsync(linksToRemove);
        }
    }
}
