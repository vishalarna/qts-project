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
    public class OnCBT_ScormUpload_DisconnectHandler : INotificationHandler<OnCBT_ScormUpload_Disconnect>
    {
        private readonly IClassScheduleEmployeeService _classScheduleEmployeeService;
        private readonly ICBT_ScormRegistrationService _cBT_ScormRegistrationService;
        private readonly ICBTService _cBTService;
        public  OnCBT_ScormUpload_DisconnectHandler(IClassScheduleEmployeeService classScheduleEmployeeService, ICBT_ScormRegistrationService cBT_ScormRegistrationService, ICBTService cBTService)
        {
            _classScheduleEmployeeService = classScheduleEmployeeService;
            _cBT_ScormRegistrationService = cBT_ScormRegistrationService;
            _cBTService = cBTService;
        }
        public async System.Threading.Tasks.Task Handle(OnCBT_ScormUpload_Disconnect cbtScormUploadDisconnect, CancellationToken cancellationToken)
        {
            var cBT = await _cBTService.GetForNotificationAsync(cbtScormUploadDisconnect.CBT_ScormUploadDisconnect.CbtId);
            var classScheduleEmployees = await _classScheduleEmployeeService.GetClassScheduleEmployeeByCBT(cBT);
            if (classScheduleEmployees != null && classScheduleEmployees.Count > 0)
            {
                foreach (var csEmp in classScheduleEmployees)
                {
                    var cbtScormRegs = await _cBT_ScormRegistrationService.FindAsync(x => x.CBTScormUploadId == cbtScormUploadDisconnect.CBT_ScormUploadDisconnect.Id &&  x.ClassScheduleEmployeeId == csEmp.Id && x.Active);
                    foreach (var cbtReg in cbtScormRegs)
                    {
                        cbtReg.Deactivate();
                    }

                    if (cbtScormRegs.Any())
                    {
                        await _cBT_ScormRegistrationService.BulkUpdateAsync(cbtScormRegs.ToList());
                    }
                }
            }

        }
    }
}
