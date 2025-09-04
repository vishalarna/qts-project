using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class TrainingIssueActionItemPriorityService : ITrainingIssueActionItemPriorityService
    {
        private readonly ITrainingIssue_ActionItemPriorityService _trainingIssue_ActionItemPriorityService;
        public TrainingIssueActionItemPriorityService(ITrainingIssue_ActionItemPriorityService trainingIssue_ActionItemPriorityService)
        {
            _trainingIssue_ActionItemPriorityService = trainingIssue_ActionItemPriorityService;
        }
        public async Task<List<TrainingIssue_ActionItemPriority_VM>> GetAllAsync()
        {
            var trainingIssueActionItemPriorities= await _trainingIssue_ActionItemPriorityService.AllAsync();
            List<TrainingIssue_ActionItemPriority_VM> trainingIssueActionItemPrioritiesVMList = new List<TrainingIssue_ActionItemPriority_VM>();
            foreach (var trainingIssueActionItemPriority in trainingIssueActionItemPriorities)
            {
                var trainingIssueActionItemPriorityVM = new TrainingIssue_ActionItemPriority_VM(trainingIssueActionItemPriority.Id, trainingIssueActionItemPriority.Priority);
                trainingIssueActionItemPrioritiesVMList.Add(trainingIssueActionItemPriorityVM);
            }
            return trainingIssueActionItemPrioritiesVMList;
        }
    }
}
