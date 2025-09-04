using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmpTestService : Common.IService<ClassSchedule_Roster_Response_Selection>
    {
        System.Threading.Tasks.Task<ClassSchedule_Roster_Response_Selection> GetEmployeeTestByIdAsync(int employeeId,int testId,int testTypeId,int questionId);

        System.Threading.Tasks.Task<List<ClassSchedule_Roster_Response_Selection>> GetQuestionAnswerByIdAsync(int employeeId, int testId, int ClassScheduleId, int questionId);

    }
}