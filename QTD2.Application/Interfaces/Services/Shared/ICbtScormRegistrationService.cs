using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_Employee;
using QTD2.Infrastructure.Model.ClassSchedule_Roster;
using QTD2.Infrastructure.Model.EmployeeTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICbtScormRegistrationService
    {
        System.Threading.Tasks.Task BulkUpdateCbtRegistrationsAsync(int classScheduleId, ClassRoasterUpdateOptions options);
        System.Threading.Tasks.Task UpdateCbtRegistrationAsync(int employeeId, ClassRoasterUpdateOptions options);
    }
}
