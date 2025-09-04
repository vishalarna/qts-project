using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class MetaILAService : Common.Service<MetaILA>, IMetaILAService
    {
        public MetaILAService(IMetaILARepository repository, IMetaILAValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsByTestIdAsync(int testId)
        {
            var metaILAList = await FindAsync(x => x.MetaILA_SummaryTest_FinalTest.TestId == testId);
            return metaILAList.ToList();
        }

        public async System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsActiveAsync()
        {
            var metaILAList = await FindAsync(r => r.Active == true);
            return metaILAList.ToList();
        }

        public async System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsCompletionHistoryAsync(List<int> metaILAIds, DateTime startDate, DateTime endDate, bool selectCompleted, bool includeInactiveILAs, bool includeInactiveEmployee)
        {
            List<Expression<Func<Domain.Entities.Core.MetaILA, bool>>> predicates = new List<Expression<Func<Domain.Entities.Core.MetaILA, bool>>>();
            predicates.Add(r => metaILAIds.Contains(r.Id));
            if (!includeInactiveILAs)
            {
                predicates.Add(r => r.Meta_ILAMembers_Links.Select(x => x.ILA.Active == true).Count() > 0);
            }
            if (!includeInactiveEmployee)
            {
                predicates.Add(r => r.MetaILA_Employees.Select(x => x.Employee.Active == true).Count() > 0);
            }
            
            var metaILAs = (await FindWithIncludeAsync(predicates, new[] { "MetaILA_Employees.Employee.Person", "Meta_ILAMembers_Links.ILA.ClassSchedules.ClassSchedule_Employee" })).ToList();
            metaILAs.Any(r => r.Meta_ILAMembers_Links.Select(r => r.ILA.ClassSchedules.Where(x => x.StartDateTime <= startDate && x.EndDateTime >= endDate)).Count() > 0);
            
            return metaILAs;
        }
        
        public async System.Threading.Tasks.Task<List<MetaILA_Employee>> GetMetaILAEmployeesByIdAsync(int id)
        {
            var metaILA= await GetWithIncludeAsync(id, new[] { "MetaILA_Employees.Employee.Person", "MetaILA_Employees.Employee.EmployeePositions.Position", "MetaILA_Employees.Employee.EmployeeOrganizations.Organization" });
            return metaILA.MetaILA_Employees.ToList();
        }

        public async Task<MetaILA> GetWithMembersAsync(int metaILAId)
        {
            var metaILA = await GetWithIncludeAsync(metaILAId, new[] { "Meta_ILAMembers_Links.ILA.ClassSchedules" });
            return metaILA;
        }

        public async Task<List<MetaILA_Employee>> GetTrainingModuleCompletionHistoryByEmployeeAsync(List<int> employeeIds, string trainingOptions, bool includeInActiveEmployee, DateTime startDate, DateTime endDate, bool includeInActiveILAs)
        {
            List<Expression<Func<MetaILA, bool>>> predicates = new List<Expression<Func<MetaILA, bool>>>();

            if (employeeIds != null)
                predicates.Add(metaILA => metaILA.MetaILA_Employees.Any(e => employeeIds.Contains(e.EmployeeId)));

            if (includeInActiveEmployee)
            predicates.Add(metaILA => metaILA.MetaILA_Employees.Any(e => !e.Employee.Active));

            if (includeInActiveILAs)
            predicates.Add(metaILA => metaILA.Meta_ILAMembers_Links.Any(e => !e.ILA.Active));

            var metaILAs = await FindWithIncludeAsync(predicates, new[] { "MetaILA_Employees.Employee.Person", "Meta_ILAMembers_Links.ILA.ClassSchedules.ClassSchedule_Employee", "Meta_ILAMembers_Links.ILA.ClassSchedules.Location" });
            var metaIlaEmployees = metaILAs.SelectMany(k => k.MetaILA_Employees);
            return metaIlaEmployees.ToList();
        }

        public async System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAsAsync()
        {
            var metaILAList = await AllWithIncludeAsync(new [] { "Meta_ILAMembers_Links" });
            return metaILAList.ToList();
        }

        public async System.Threading.Tasks.Task<List<MetaILA>> GetMetaILAByIDAsync(List<int> metaILAIds)
        {
            List<Expression<Func<MetaILA, bool>>> predicates = new List<Expression<Func<MetaILA, bool>>>();
            predicates.Add(r => metaILAIds.Contains(r.Id));
            var metaILAs = (await FindWithIncludeAsync(predicates, new[] { "Meta_ILAMembers_Links.MetaILAConfigurationPublishOption", "MetaILA_SummaryTest_FinalTest.Test", "StudentEvaluation", "MetaILAStatus" })).ToList();
            return metaILAs.ToList();
        }

        public async Task<MetaILA> GetMetaILAWithMembersOnlyAsync(int metaILAId)
        {
            var metaILA = await GetWithIncludeAsync(metaILAId, new[] { "Meta_ILAMembers_Links" });
            return metaILA;
        }
    }
}
    
