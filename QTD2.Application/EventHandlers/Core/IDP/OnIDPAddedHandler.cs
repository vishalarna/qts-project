using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnIDPAddedHandler : INotificationHandler<OnIDPAdded>
    {
        private readonly Domain.Interfaces.Service.Core.IIDPService _iDPService;

        private readonly Domain.Interfaces.Service.Core.IClassScheduleEmployeeService _classScheduleEmployeeService;

        public OnIDPAddedHandler(IIDPService iDPService, IIDPScheduleService iDPScheduleService, Domain.Interfaces.Service.Core.IClassScheduleEmployeeService classScheduleEmployeeService)
        {
            _iDPService = iDPService;
            _classScheduleEmployeeService = classScheduleEmployeeService;
        }

        public async System.Threading.Tasks.Task Handle(OnIDPAdded payload, CancellationToken cancellationToken)
        {
            var classScheduleEmployees = await _classScheduleEmployeeService.GetClassSchedulesForIDP(payload.IDP.ILAId, payload.IDP.IDPYear.Value, payload.IDP.EmployeeId);

            foreach (var classScheduleEmployee in classScheduleEmployees)
            {
                var idpSchedule = new Domain.Entities.Core.IDPSchedule(
                        payload.IDP.Id,
                        classScheduleEmployee.ClassScheduleId,
                        classScheduleEmployee.ClassSchedule.StartDateTime,
                        classScheduleEmployee.ClassSchedule.EndDateTime,
                        classScheduleEmployee.PlannedDate);

                payload.IDP.AddIdpSchedule(idpSchedule);
            }

            await _iDPService.UpdateAsync(payload.IDP);
        }
    }
}
