using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Application.EventHandlers.Core
{
    public class ClassSchedule_Employee_UnenrolledHandler : INotificationHandler<OnClassSchedule_Employee_Unenrolled>
    {
        private readonly Domain.Interfaces.Service.Core.IIDPScheduleService _iDPScheduleService;
        private readonly Domain.Interfaces.Service.Core.IIDPService _iDPService;
        private readonly ICBT_ScormRegistrationService _cBT_ScormRegistrationService;

        public ClassSchedule_Employee_UnenrolledHandler(Domain.Interfaces.Service.Core.IIDPService iDPService, IIDPScheduleService iDPScheduleService, ICBT_ScormRegistrationService cBT_ScormRegistrationService)

        {
            _iDPService = iDPService;
            _iDPScheduleService = iDPScheduleService;
            _cBT_ScormRegistrationService = cBT_ScormRegistrationService;
        }

        public async Task Handle(OnClassSchedule_Employee_Unenrolled classScheduleEmployee, CancellationToken cancellationToken)
        {
            await RemoveScormRegistration(classScheduleEmployee);
            var idpSchedules = await _iDPScheduleService.FindAsync(r => r.IDP.EmployeeId == classScheduleEmployee.ClassSchedule_Employee.EmployeeId && r.ClassScheduleId == classScheduleEmployee.ClassSchedule_Employee.ClassScheduleId);

            foreach (var idpSchedule in idpSchedules)
            {
                idpSchedule.Deactivate();
                idpSchedule.Delete();
                await _iDPScheduleService.UpdateAsync(idpSchedule);
            }
        }

        protected async System.Threading.Tasks.Task RemoveScormRegistration(OnClassSchedule_Employee_Unenrolled payload)
        {
            var scormRegistrations = (await _cBT_ScormRegistrationService.FindAsync(x => x.ClassScheduleEmployeeId == payload.ClassSchedule_Employee.Id)).ToList();
            foreach (var scormReg in scormRegistrations)
            {
                scormReg.Delete();
            }
            await _cBT_ScormRegistrationService.BulkUpdateAsync(scormRegistrations);
        }
    }
}
