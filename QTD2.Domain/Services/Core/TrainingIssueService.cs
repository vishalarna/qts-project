using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class TrainingIssueService : Common.Service<TrainingIssue>, ITrainingIssueService
    {
        public TrainingIssueService(ITrainingIssueRepository repository, ITrainingIssueValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<TrainingIssue> GetWithAllIncludesAsync(int id)
        {
            var trainingIssue = await GetWithIncludeAsync(id, new[] { "Status", "Severity", "DriverType.DriverSubTypes", "ActionItems.Status", "ActionItems.Status", "ActionItems.Priority", "ActionItems" });
            return trainingIssue;
        }

        public async Task<TrainingIssue> GetForCopy(int id)
        {
            var traineeIssueCopy = await FindWithIncludeAsync(r => r.Id == id, new string[] { "Status", "Severity", "DriverType", "ActionItems", "DataElement", "DriverSubType" });
            return traineeIssueCopy.First();
        }

        public async Task<List<TrainingIssue>> GetAllTrainingIssueAsync()
        {
            var trainingIssue = await AllWithIncludeAsync(new string[] { "Status", "DriverType.DriverSubTypes", "DataElement", "Severity" });
            return trainingIssue.ToList();
        }

        public async Task<List<TrainingIssue>> GetTrainingIssueDetailsByIdAsync(List<int> trainingIssueIds)
        {
            List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>>();
            predicates.Add(t => trainingIssueIds.Contains(t.Id));
            var trainingIssues = (await FindWithIncludeAsync(predicates, new[] { "Status", "Severity", "DriverType", "DriverSubType", "ActionItems.Status", "ActionItems.Priority", "ActionItems", "DataElement" })).ToList();
            return trainingIssues;
        }

        public async Task<List<TrainingIssue>> GetTrainingIssueActionItemsAsync(List<int> trainingIssueIds, string actionStepStatus)
        {
            List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>>();
            predicates.Add(t => trainingIssueIds.Contains(t.Id));

            var trainingIssues = (await FindWithIncludeAsync(predicates, new[] { "Status", "Severity", "DriverType", "DriverSubType", "ActionItems.Status", "ActionItems.Priority", "ActionItems", "DataElement" })).ToList();

            if (int.TryParse(actionStepStatus?.Trim(), out int statusId))
            {
                foreach (var ti in trainingIssues)
                {
                    ti.ActionItems = ti.ActionItems.Where(ai => ai.StatusId == statusId).ToList();
                }
            }
            return trainingIssues;
        }

        public async Task<List<TrainingIssue>> GetTrainingIssuesByStatusAndSeverityIdAsync(List<string>types,List<int> severityLevelIds, string status)
        {
            List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>>();
            predicates.Add(tr => severityLevelIds.Contains(tr.SeverityId));
            if (status == "Open")
            {
                predicates.Add(tr => tr.Status.Status == "Open");
            }
            if (status == "Closed")
            {
                predicates.Add(tr => tr.Status.Status == "Closed");
            }
            var allowedTypes = types.Select(t => $"DataElement_{t}").ToList();
            predicates.Add(tr => allowedTypes.Contains(EF.Property<string>(tr.DataElement, "DataElementType")));
            return (await FindWithIncludeAsync(predicates, new string[] { "Status", "Severity", "DriverType", "DriverSubType", "ActionItems", "DataElement" }, true)).ToList();
        }

        public async Task<List<TrainingIssue>> GetAllTrainingIssuesByDataElementTypeAsync(string type)
        {
            List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>>();
            predicates.Add(tr => EF.Property<string>(tr.DataElement, "DataElementType") == $"DataElement_{type}");
            return (await FindWithIncludeAsync(predicates, new string[] { "Status", "Severity", "DriverType", "DriverSubType", "ActionItems", "DataElement" }, true)).ToList();
        }

        public async Task<TrainingIssue> GetByTaskReviewIdAsync(int taskReviewId)
        {
            List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.TrainingIssue, bool>>>();
            predicates.Add(tr => tr.TaskReviewId == taskReviewId);
            predicates.Add(r => !r.Deleted);
            var trainingIssue = await FindWithIncludeAsync(r => r.TaskReviewId == taskReviewId, new[] { "Status", "Severity", "DriverType.DriverSubTypes", "ActionItems.Status", "ActionItems.Priority"});
            return trainingIssue.FirstOrDefault();
        }

        public async Task<TrainingIssue> GetTrainingIssueByTaskReviewIdAsync(int taskReviewId)
        {
            var traineeIssues = await FindWithIncludeAsync(r => r.TaskReviewId == taskReviewId, new string[] { "ActionItems", "DataElement" });
            return traineeIssues.FirstOrDefault();
        }

    }
}
