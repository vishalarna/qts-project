using MediatR;
using QTD2.Domain.Entities.Core;
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
    public class OnClassSchedule_UpdateHandler : INotificationHandler<OnClassSchedule_Update>
    {
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly IIDPScheduleService _iIDPScheduleService;

        public OnClassSchedule_UpdateHandler(IClassScheduleEmployeeService classScheduleEmployeeService, IIDPScheduleService iIDPScheduleService)
        {
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _iIDPScheduleService = iIDPScheduleService;
        }

        public async System.Threading.Tasks.Task Handle(OnClassSchedule_Update classSchedule, CancellationToken cancellationToken)
        {
            var idpSchedules = await _iIDPScheduleService.GetIDPSchedulesByClassScheduleIdAsync(classSchedule.ClassSchedule.Id);
            if (idpSchedules != null && idpSchedules.Any())
            {
                foreach (var idpSchedule in idpSchedules)
                {
                    idpSchedule.startDate = classSchedule.ClassSchedule.StartDateTime;
                    idpSchedule.endDate = classSchedule.ClassSchedule.EndDateTime;
                    await _iIDPScheduleService.UpdateAsync(idpSchedule);
                }
            }
        }

    }
}