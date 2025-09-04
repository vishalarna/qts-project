using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IMetaILAService : Common.IService<MetaILA>
    {
        public System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsByTestIdAsync(int testId);
        public System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsActiveAsync();

        public System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsCompletionHistoryAsync(List<int> metaILAIds, DateTime startDate, DateTime endDate, bool selectCompleted, bool includeInactiveILAs, bool includeInactiveEmployee);
        public System.Threading.Tasks.Task<List<MetaILA_Employee>> GetMetaILAEmployeesByIdAsync(int id);
        public System.Threading.Tasks.Task<MetaILA> GetWithMembersAsync(int metaILAId);
        System.Threading.Tasks.Task<List<MetaILA_Employee>> GetTrainingModuleCompletionHistoryByEmployeeAsync(List<int> employee, string trainingOptions, bool includeInActiveEmployee, DateTime startDate, DateTime endDate, bool includeInActiveILAs);
        public System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsAsync();
        public System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAByIDAsync(List<int> metaILAIds);
        public System.Threading.Tasks.Task<MetaILA> GetMetaILAWithMembersOnlyAsync(int metaILAId);
    }
}
