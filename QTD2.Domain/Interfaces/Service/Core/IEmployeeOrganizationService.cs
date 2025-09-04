using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmployeeOrganizationService : Common.IService<EmployeeOrganization>
    {

        System.Threading.Tasks.Task<List<EmployeeOrganization>> AllActiveEmpOrganizationsWithOrganizationAndConditions(Expression<Func<EmployeeOrganization, bool>> predicate);

        System.Threading.Tasks.Task<List<EmployeeOrganization>> GetEMPOrgIdsOnly(Expression<Func<EmployeeOrganization, bool>> predicate);
        System.Threading.Tasks.Task<List<EmployeeOrganization>> GetEmployeeOrganizationsByOrganizationIdAsync(int organizationId);
    }
}
