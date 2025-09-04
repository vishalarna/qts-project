using MediatR;
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
    public class OnClassSchedule_Employee_DeletedHandler : INotificationHandler<OnClassSchedule_Employee_Deleted>
    {
        private readonly IDocumentService _documentService;
        private readonly IIDPScheduleService _iDPScheduleService;
        public OnClassSchedule_Employee_DeletedHandler(IDocumentService documentService, IIDPScheduleService iDPScheduleService)
        {
            _documentService = documentService;
            _iDPScheduleService = iDPScheduleService;
        }

        public async Task Handle(OnClassSchedule_Employee_Deleted deletedClassScheduleEmployee, CancellationToken cancellationToken)
        {
            await DeleteIDPSchedules(deletedClassScheduleEmployee);
            var documents = await _documentService.GetActiveByLinkedDataAsync("ClassScheduleEmployees", deletedClassScheduleEmployee.ClassSchedule_Employee.Id);
            foreach (var doc in documents)
            {
                doc.Delete();
            }
            await _documentService.BulkUpdateAsync(documents);
        }

        private async Task DeleteIDPSchedules(OnClassSchedule_Employee_Deleted deletedClassScheduleEmployee)
        {
            var idpSchedules = await _iDPScheduleService.FindAsync(r => r.IDP.EmployeeId == deletedClassScheduleEmployee.ClassSchedule_Employee.EmployeeId && r.ClassScheduleId == deletedClassScheduleEmployee.ClassSchedule_Employee.ClassScheduleId);

            foreach (var idpSchedule in idpSchedules)
            {
                idpSchedule.Deactivate();
                idpSchedule.Delete();
                await _iDPScheduleService.UpdateAsync(idpSchedule);
            }
        }
    }
}
