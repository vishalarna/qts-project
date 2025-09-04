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
    public class OnCBT_ScormUpload_ConnectHandler : INotificationHandler<OnCBT_ScormUpload_Connect>
    {
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly ICBT_ScormRegistrationService _cBT_ScormRegistrationService;
        public OnCBT_ScormUpload_ConnectHandler(IClassScheduleEmployeeService classScheduleEmployeeService, ICBT_ScormRegistrationService cBT_ScormRegistrationService)
        {
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _cBT_ScormRegistrationService = cBT_ScormRegistrationService;
        }

        public async System.Threading.Tasks.Task Handle(OnCBT_ScormUpload_Connect cbtScormUpload, CancellationToken cancellationToken)
        {
            var classScheduleEmployees = await _classScheduleEmployeeService.GetClassScheduleEmployeeByCBT(cbtScormUpload.CBT_ScormUpload.CBT);
            if (classScheduleEmployees.Any() && classScheduleEmployees != null)
            {
                foreach (var csEmp in classScheduleEmployees)
                {
                    var existingRegistrations = await _cBT_ScormRegistrationService.FindAsync(r => r.ClassScheduleEmployeeId == csEmp.Id && r.CBTScormUploadId == cbtScormUpload.CBT_ScormUpload.Id && !r.Active);
                    var existingRegistration = existingRegistrations.FirstOrDefault();
                    if (existingRegistration != null) 
                    {
                        existingRegistration.Activate();
                        await _cBT_ScormRegistrationService.UpdateAsync(existingRegistration);
                    }
                    else
                    {
                        var cBT_ScormRegistration = new CBT_ScormRegistration(cbtScormUpload.CBT_ScormUpload.Id, csEmp.Id);
                        await _cBT_ScormRegistrationService.AddAsync(cBT_ScormRegistration);
                    }
                }
            }
        }
    }
}
