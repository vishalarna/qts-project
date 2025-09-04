using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeOrganizationService : Common.Service<EmployeeOrganization>, IEmployeeOrganizationService
    {
        public EmployeeOrganizationService(IEmployeeOrganizationRepository employeeOrganizationRepository, IEmployeeOrganizationValidation employeeOrganizationValidation)
            : base(employeeOrganizationRepository, employeeOrganizationValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<EmployeeOrganization>> AllActiveEmpOrganizationsWithOrganizationAndConditions(Expression<Func<EmployeeOrganization, bool>> predicate)
        {
            var empOrgs = await FindQueryWithIncludeAsync(predicate, new string[] { "Organization" }).ToListAsync();
            return empOrgs;
        }

        public async System.Threading.Tasks.Task<List<EmployeeOrganization>> GetEMPOrgIdsOnly(Expression<Func<EmployeeOrganization, bool>> predicate)
        {
            var empOrgs = await FindQuery(predicate,true).Select(s => new EmployeeOrganization { Id = s.Id, EmployeeId = s.EmployeeId, OrganizationId = s.OrganizationId }).ToListAsync();
            return empOrgs;
        }

        public async System.Threading.Tasks.Task<List<EmployeeOrganization>> GetEmployeeOrganizationsByOrganizationIdAsync(int organizationId)
        {
            var empOrgs = await FindAsync(r => r.OrganizationId == organizationId);
            return empOrgs.ToList();
        }
    }
}
