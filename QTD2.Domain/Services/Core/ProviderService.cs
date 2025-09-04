using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QTD2.Domain.Services.Core
{
    public class ProviderService : Common.Service<Provider>, IProviderService
    {
        public ProviderService(IProviderRepository repository, IProviderValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<IEnumerable<Provider>> GetByProvidersListAsync(List<int> providerIds)
        {
            return await FindAsync(r => providerIds.Contains(r.Id));
        }

        public async System.Threading.Tasks.Task<Provider> GetProviderByIdAsync(int providerId)
        {
            var provider = await FindAsync(x => x.Id == providerId);
            return provider.FirstOrDefault();
        }

        public async Task<List<Provider>> GetCompactedProvider()
        {
            var providers = await AllQuery().Select(s => new Provider
            {
                Active = s.Active,
                Id = s.Id,
                Number = s.Number,
                Name = s.Name,
                IsPriority = s.IsPriority,
                IsNERC = s.IsNERC,
            }).ToListAsync();

            return providers;
        }

        public async Task<Provider> GetCompactedProviderById(int? providerId)
        {
            var providers = await FindQuery(x => x.Id == providerId, true).Select(s => new Provider
            {
                Active = s.Active,
                Id = s.Id,
                Number = s.Number,
                Name = s.Name,
                IsPriority = s.IsPriority,
            }).FirstOrDefaultAsync();

            return providers;
        }

        public async Task<List<Provider>> GetProvidersWithILAsAndClassSchedules()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "ILAs.ClassSchedules" });
            return queryable.ToList();
        }

        public async Task<List<Provider>> GetProvidersWithILAClassScheduleEmployees()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active && x.ILAs.Where(r => r.Active).Any(), new string[] { "ILAs.ClassSchedules.ClassSchedule_Employee.Employee.Person" });
            return queryable.ToList();
        }

        public async Task<List<Provider>> GetProvidersWithILAClassScheduleEmployeesAndTests()
        {
            //var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "ILAs.ClassSchedules.ClassSchedule_Employee.Employee.Person", "ILAs.ClassSchedules.ClassSchedule_Employee.Employee.ClassSchedule_Rosters.Test" });
            var queryable = await FindWithIncludeAsync(x => x.Active && x.ILAs.Where(r => r.Active).Any(), new string[] { "ILAs.ClassSchedules.ClassSchedule_Employee.Employee.Person" });
            var queryableWithTests = await FindWithIncludeAsync(x => x.Active && x.ILAs.Where(r => r.Active).Any(), new string[] { "ILAs.ClassSchedules.ClassSchedule_Rosters.Test" });


            foreach(var provider in queryable)
            {
                foreach(var ila in provider.ILAs)
                {
                    foreach(var cs in ila.ClassSchedules)
                    {
                        cs.ClassSchedule_Rosters = queryableWithTests.Where(r => r.Id == provider.Id).First().ILAs.Where(r => r.Id == ila.Id).First().ClassSchedules.Where(r => r.Id == cs.Id).First().ClassSchedule_Rosters.ToList();
                    }
                }
            }
            
            return queryable.ToList();
        }

        public async Task<List<Provider>> GetFilteredProvidersAsync(string filter, bool activeStatus, List<int> providerIds)
        {
            var providerPredicates = new List<Expression<Func<Provider, bool>>>();
            if (!string.IsNullOrEmpty(filter))
            {
                providerPredicates.Add(r =>
                    r.Name.ToUpper().Contains(filter.ToUpper()) ||
                    r.ILAs.Any(s => s.Name.ToUpper().Contains(filter.ToUpper())) ||
                    r.ILAs.Any(s => s.Number.ToUpper().Contains(filter.ToUpper())));
            }
            if (activeStatus)
            {
                providerPredicates.Add(r => r.Active);
            }
            else
            {
                providerPredicates.Add(r => !r.Active);
            }
            if (providerIds.Any())
            {
                providerPredicates.Add(r => providerIds.Contains(r.Id));
            }
            var providers = (await FindAsync(providerPredicates, true)).ToList();
            return providers.ToList();
        }

    }
}
