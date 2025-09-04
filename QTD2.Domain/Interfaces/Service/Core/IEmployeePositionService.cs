using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmployeePositionService : Common.IService<EmployeePosition>
    {
        //System.Threading.Tasks.Task<List<EmployeePosition>> GetEmployeesPositionsAsync(string active);

        System.Threading.Tasks.Task<int> GetTraineeEmployees();
        System.Threading.Tasks.Task<List<EmployeePosition>> GetEmployeesPositionsAsync(string active, List<int> employees);

        System.Threading.Tasks.Task<List<EmployeePosition>> GetEmpPositionsWithCompactPositionsAndConditions(Expression<Func<EmployeePosition, bool>> predicates);

        System.Threading.Tasks.Task<List<EmployeePosition>> GetEMPPositionsIdsOnly(Expression<Func<EmployeePosition, bool>> predicates);
    }
}
