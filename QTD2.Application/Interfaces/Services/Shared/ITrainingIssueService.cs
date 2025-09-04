using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingIssueService
    {
        public Task<TrainingIssueOverview_VM> GetOverviewAsync();
        public Task<List<TrainingIssue_VM>> GetWithPendingActionItemsAsync();
        public Task<List<TrainingIssue_VM>> GetWithNoActionItemsAsync();
        public Task<TrainingIssue_VM> CreateAsync(TrainingIssue_VM options);
        public Task<TrainingIssue_VM> GetAsync(int id);
        public System.Threading.Tasks.Task<object> CopyAsync(int id);
        public System.Threading.Tasks.Task ActivateAsync(int id);
        public System.Threading.Tasks.Task InactivateAsync(int id);
        public System.Threading.Tasks.Task DeleteAsync(int trainingIssueId);
        public Task<TrainingIssue_VM> UpdateAsync(int id, TrainingIssue_VM options);
        public Task<List<TrainingIssue_ActionItem_VM>> UpdateActionItemsAsync(int id, TrainingIssue_ActionItems_VM options, bool isStatusCheck);
        public Task<TrainingIssue_DataElement_VM> UpdateDataElementAsync(int id, TrainingIssue_DataElement_VM option);
        public List<TrainingIssue_DataElementCategory_VM> GetAllDataElementsWithCategories();
        public Task<List<TrainingIssue_VM>> GetTrainingIssueByDataElementTypeAndTypeIdAsync(int id,string type);
        public Task<TrainingIssue_VM> GetTrainingIssueByTaskReviewIdAsync(int taskReviewId);

    }
}
