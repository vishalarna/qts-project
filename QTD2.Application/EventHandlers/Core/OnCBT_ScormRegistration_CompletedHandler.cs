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
    public  class OnCBT_ScormRegistration_CompletedHandler : INotificationHandler<OnCBT_ScormRegistration_Completed>
    {
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly ICBT_ScormRegistrationService _cBT_ScormRegistrationService;
        public OnCBT_ScormRegistration_CompletedHandler(IClassScheduleEmployeeService classScheduleEmployeeService, ICBT_ScormRegistrationService cBT_ScormRegistrationService)
        {
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _cBT_ScormRegistrationService = cBT_ScormRegistrationService;
        }
        public async System.Threading.Tasks.Task Handle(OnCBT_ScormRegistration_Completed cBTScormRegistration, CancellationToken cancellationToken)
        {
            var registration = await _cBT_ScormRegistrationService.GetByIdAsync(cBTScormRegistration.CBT_ScormRegistration.Id);
            if (registration == null || registration.ClassScheduleEmployee == null)
                return;

            var classScheduleEmployee = registration.ClassScheduleEmployee;

            if (classScheduleEmployee.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings?.UsePreTestAndTest == true || classScheduleEmployee.ClassSchedule?.ClassSchedule_TestReleaseEMPSettings?.FinalTestId == null)
            {
                classScheduleEmployee.CompleteClass(registration.CompletedDate, registration.Grade, Convert.ToInt32(registration.Score),"");
                await _classScheduleEmployeeService.UpdateAsync(classScheduleEmployee);
            }
        }
    }
}
