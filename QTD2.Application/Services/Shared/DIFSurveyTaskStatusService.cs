using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class DIFSurveyTaskStatusService : IDIFSurveyTaskStatusService
    {
        private readonly IDIFSurvey_Task_StatusService _dIFSurvey_Task_StatusService;
        public DIFSurveyTaskStatusService(IDIFSurvey_Task_StatusService dIFSurvey_Task_StatusService)
        {
            _dIFSurvey_Task_StatusService = dIFSurvey_Task_StatusService;
        }
        public async Task<List<DIFSurvey_Task_Status>> GetTaskStatus()
        {
            var difTaskStatus = await _dIFSurvey_Task_StatusService.AllAsync();
            var filteredStatus = difTaskStatus.Where(r => r.Status.ToLower() != "no responses yet");
            return filteredStatus.ToList();
        }
    }
}
