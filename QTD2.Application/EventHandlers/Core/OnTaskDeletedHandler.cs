using MediatR;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnTaskDeletedHandler : INotificationHandler<OnTaskDeleted>
    {
        private readonly QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService _taskQualificationService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService _position_TaskService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService _ila_TaskObjective_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService _task_EnablingObjective_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService _procedure_Task_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService _safetyHazard_Task_LinkService;
        private readonly QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService _rr_Task_LinkService;




        public OnTaskDeletedHandler(
            QTD2.Domain.Interfaces.Service.Core.ITaskQualificationService taskQualificationService,
            QTD2.Domain.Interfaces.Service.Core.IPosition_TaskService position_TaskService,
            QTD2.Domain.Interfaces.Service.Core.IILA_TaskObjective_LinkService ila_TaskObjective_LinkService,
            QTD2.Domain.Interfaces.Service.Core.ITask_EnablingObjective_LinkService task_EnablingObjective_LinkService,
            QTD2.Domain.Interfaces.Service.Core.IProcedure_Task_LinkService procedure_Task_LinkService,
            QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Task_LinkService safetyHazard_Task_LinkService,
            QTD2.Domain.Interfaces.Service.Core.IRR_Task_LinkService rr_Task_LinkService
            )
        {
            _taskQualificationService = taskQualificationService;
            _position_TaskService = position_TaskService;
            _ila_TaskObjective_LinkService = ila_TaskObjective_LinkService;
            _task_EnablingObjective_LinkService = task_EnablingObjective_LinkService;
            _procedure_Task_LinkService = procedure_Task_LinkService;
            _safetyHazard_Task_LinkService = safetyHazard_Task_LinkService;
            _rr_Task_LinkService = rr_Task_LinkService;
        }

        public async System.Threading.Tasks.Task Handle(OnTaskDeleted taskDeleted, CancellationToken cancellationToken)
        {
            var taskQualifications = await _taskQualificationService.GetByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var positiontasks = await _position_TaskService.GetPositionTaskByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var ilaTaskObjectiveLinks = await _ila_TaskObjective_LinkService.GetILATaskObjectiveLinkByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var taskEnablingObjectiveLinks = await _task_EnablingObjective_LinkService.GetTaskEnablingObjectiveLinksByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var procedure_Task_Links = await _procedure_Task_LinkService.GetProcedureTaskLinksByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var safetyHazard_Task_Links = await _safetyHazard_Task_LinkService.GetSafetyHazardTaskLinksByTaskIdAsync(taskDeleted.TaskDeleted.Id);
            var rr_Task_Links = await _rr_Task_LinkService.GetRRTaskLinksByTaskIdAsync(taskDeleted.TaskDeleted.Id);

            foreach (var taskQualification in taskQualifications)
            {
                taskQualification.Delete();
                await _taskQualificationService.UpdateAsync(taskQualification);
            }

            foreach (var position_Task in positiontasks)
            {
                position_Task.Delete();
            }
            await _position_TaskService.BulkUpdateAsync(positiontasks);

            foreach (var ilaTaskObjectiveLink in ilaTaskObjectiveLinks)
            {
                ilaTaskObjectiveLink.Delete();
            }
            await _ila_TaskObjective_LinkService.BulkUpdateAsync(ilaTaskObjectiveLinks);

            foreach (var taskEnablingObjectiveLink in taskEnablingObjectiveLinks)
            {
                taskEnablingObjectiveLink.Delete();
            }
            await _task_EnablingObjective_LinkService.BulkUpdateAsync(taskEnablingObjectiveLinks);

            foreach (var procedure_Task_Link in procedure_Task_Links)
            {
                procedure_Task_Link.Delete();
            }
            await _procedure_Task_LinkService.BulkUpdateAsync(procedure_Task_Links);

            foreach (var safetyHazard_Task_Link in safetyHazard_Task_Links)
            {
                safetyHazard_Task_Link.Delete();
            }
            await _safetyHazard_Task_LinkService.BulkUpdateAsync(safetyHazard_Task_Links);

            foreach (var rr_Task_Link in rr_Task_Links)
            {
                rr_Task_Link.Delete();
            }
            await _rr_Task_LinkService.BulkUpdateAsync(rr_Task_Links);

        }
    }
}