using MediatR;
using QTD2.Domain.Events.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core.ClassSchedule_Employee
{
    public class ClassSchedule_Employee_UpdatedCompletionInfoHandler : INotificationHandler<OnClassSchedule_Employee_UpdatedCompletionInfo>
    {
        IIDPScheduleService _iDPScheduleService;
        public ClassSchedule_Employee_UpdatedCompletionInfoHandler(IIDPScheduleService iDPScheduleService)
        {
            _iDPScheduleService = iDPScheduleService;
        }
        public async Task Handle(OnClassSchedule_Employee_UpdatedCompletionInfo payload, CancellationToken cancellationToken)
        {
         
            var classEmployee = payload.ClassSchedule_Employee;
            var idpSchedule = (await _iDPScheduleService.GetIDPSchedulesByClassIdAndEmpIdAsync(classEmployee.ClassScheduleId, classEmployee.EmployeeId));
            idpSchedule.UpdateGradeRelatedData(classEmployee.FinalGrade, classEmployee.GradeNotes, classEmployee.FinalScore?.ToString(), classEmployee.PopulateOJTRecord, classEmployee.CompletionDate);
            await _iDPScheduleService.UpdateAsync(idpSchedule);
        
        }
    }
}
