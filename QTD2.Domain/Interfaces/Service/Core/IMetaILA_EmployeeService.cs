using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IMetaILA_EmployeeService : Common.IService<MetaILA_Employee>
    {
        System.Threading.Tasks.Task<List<MetaILA_Employee>> GetByIlaIdAndEmployeeId(int iLAID, int employeeId);
        System.Threading.Tasks.Task<List<MetaILA_Employee>> GetByEmployeeId(int employeeId);
    }
}
