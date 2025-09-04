using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITaskReQualificationEmp_SignOffService : Common.IService<TaskReQualificationEmp_SignOff>
    {
        public Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffByEmployeeId(int employeeId);
        public Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffByTQId(int taskQualificationId);
        public Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationsEmp_SignOffByTQId(int taskQualificationId, int evaluatorId);
        public Task<List<TaskReQualificationEmp_SignOff>> GetTaskReQualificationEmp_SignOffListByTQIds(List<int> tqIds);
    }
}
