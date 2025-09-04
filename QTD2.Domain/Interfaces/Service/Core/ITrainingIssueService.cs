using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingIssueService : Common.IService<TrainingIssue>
    {
        Task<TrainingIssue> GetWithAllIncludesAsync(int id);
        System.Threading.Tasks.Task<TrainingIssue> GetForCopy(int id);
        Task<List<TrainingIssue>> GetAllTrainingIssueAsync();
        Task<List<TrainingIssue>> GetTrainingIssueDetailsByIdAsync(List<int> trainingIssueIds);
        Task<List<TrainingIssue>> GetTrainingIssueActionItemsAsync(List<int> trainingIssueIds, string actionStepStatus);
        Task<List<TrainingIssue>> GetTrainingIssuesByStatusAndSeverityIdAsync(List<string> types,List<int> severityLevelIds, string status);
        Task<List<TrainingIssue>> GetAllTrainingIssuesByDataElementTypeAsync(string type);
        Task<TrainingIssue> GetByTaskReviewIdAsync(int taskReviewId);
        Task<TrainingIssue> GetTrainingIssueByTaskReviewIdAsync(int taskReviewId);
    }
}
