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
        public OnILA_Deleted_Handler(IClassScheduleService classScheduleService, IInstructorWorkbook_ProspectiveILAService instructorWorkbook_ProspectiveILAService) {
            _classScheduleService = classScheduleService;
            _instructorWorkbook_ProspectiveILAService = instructorWorkbook_ProspectiveILAService;
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
        }
    }
}
