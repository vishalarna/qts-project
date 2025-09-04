using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.PositionTask;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IPositionTaskService
    {
        public System.Threading.Tasks.Task MarkPositionTasksAsR6(UpdateMarkAsR6Model updateMarkAsR6Model);
        public System.Threading.Tasks.Task UnmarkPositionTaskAsR6(int positionTaskId, UpdateUnmarkAsR6Model updateUnmarkAsR6Model);
        public System.Threading.Tasks.Task UpdateR5Tasks(int positionTaskId, LinkR5UpdateTasksModel linkR5UpdateTasksModel);
        public System.Threading.Tasks.Task DeleteR5Task(int positionTaskId, int r5ImpactedTaskId, DeleteR5TaskModel deleteR5TaskModel);
    }
}