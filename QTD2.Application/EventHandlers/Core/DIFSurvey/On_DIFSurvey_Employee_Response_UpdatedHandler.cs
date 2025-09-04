using MediatR;
using Microsoft.Extensions.Logging;
using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Service.Core;


namespace QTD2.Application.EventHandlers.Core
{
  public  class On_DIFSurvey_Employee_Response_UpdatedHandler : INotificationHandler<On_DIFSurvey_Employee_Response_Updated>
    {
        private readonly ILogger<On_DIFSurvey_Employee_Response_UpdatedHandler> _logger;
        private readonly IDIFSurvey_TaskService _difTaskDomainService;
        public On_DIFSurvey_Employee_Response_UpdatedHandler(
           IDIFSurvey_TaskService difTaskDomainService
            )
        {
            _difTaskDomainService = difTaskDomainService;
        }
        public async System.Threading.Tasks.Task Handle(On_DIFSurvey_Employee_Response_Updated payload, CancellationToken cancellationToken)
        {
            try
            {
                var difSurveyTask = await _difTaskDomainService.GetWithIncludeAsync(payload.DIFSurvey_Employee_Response.DIFSurvey_TaskId, new string[] { "Responses.DIFSurvey_Employee" });
                if(difSurveyTask != null)
                {
                    difSurveyTask.Update();
                    await _difTaskDomainService.UpdateAsync(difSurveyTask);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("", e);
            }
        }
    }
}
