using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnILA_Deleted_Handler : INotificationHandler<OnILA_Deleted>
    {
        private readonly IClassScheduleService _classScheduleService;
        private readonly IInstructorWorkbook_ProspectiveILAService _instructorWorkbook_ProspectiveILAService;
        private readonly ISimulatorScenario_ILAService _simulatorScenario_ILAService;
        private readonly ISimulatorScenario_PrerequisiteService _simulatorScenario_PrerequisiteService;
        public OnILA_Deleted_Handler(IClassScheduleService classScheduleService, IInstructorWorkbook_ProspectiveILAService instructorWorkbook_ProspectiveILAService, ISimulatorScenario_ILAService simulatorScenario_ILAService, ISimulatorScenario_PrerequisiteService simulatorScenario_PrerequisiteService) {
            _classScheduleService = classScheduleService;
            _instructorWorkbook_ProspectiveILAService = instructorWorkbook_ProspectiveILAService;
            _simulatorScenario_ILAService = simulatorScenario_ILAService;
            _simulatorScenario_PrerequisiteService = simulatorScenario_PrerequisiteService;
    }

        public async System.Threading.Tasks.Task Handle(OnILA_Deleted ila, CancellationToken cancellationToken)
        {
            var classSchedules = await _classScheduleService.GetClassSchedulesByIdAsync(ila.DeletedILA.Id);

            if (classSchedules.Count != 0 && classSchedules != null)
            {
                foreach (var cs in classSchedules)
                {
                    cs.Delete();
                }
                await _classScheduleService.BulkUpdateAsync(classSchedules);
            }

            var iwbProspectiveILA = await _instructorWorkbook_ProspectiveILAService.GetIWBProspectiveILAByILAId(ila.DeletedILA.Id);
            if(iwbProspectiveILA != null)
            {
                iwbProspectiveILA.Delete();
                await _instructorWorkbook_ProspectiveILAService.UpdateAsync(iwbProspectiveILA);
            }

            var simulatorScenario_ILAs = await _simulatorScenario_ILAService.GetSimulatorScenarioILAByILAIdAsync(ila.DeletedILA.Id);
            if (simulatorScenario_ILAs.Count() > 0)
            {
                foreach (var simulatorScenario_ILA in simulatorScenario_ILAs)
                {
                    simulatorScenario_ILA.Delete();
                    await _simulatorScenario_ILAService.UpdateAsync(simulatorScenario_ILA);
                }
            }

            var simulatorScenario_Prerequisites = await _simulatorScenario_PrerequisiteService.GetPrerequisiteByILAIdAsync(ila.DeletedILA.Id);
            if (simulatorScenario_Prerequisites.Count() > 0)
            {
                foreach (var simulatorScenario_Prerequisite in simulatorScenario_Prerequisites)
                {
                    simulatorScenario_Prerequisite.Delete();
                    await _simulatorScenario_PrerequisiteService.UpdateAsync(simulatorScenario_Prerequisite);
                }
            }
        }
    }
}
