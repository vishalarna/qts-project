using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class OrganizationService : Common.Service<Organization>, IOrganizationService
    {
        public OrganizationService(IOrganizationRepository organizationRepository, IOrganizationValidation organizationValidation)
            : base(organizationRepository, organizationValidation)
        {

        }
        public async System.Threading.Tasks.Task<IEnumerable<Organization>> GetByIdListAsync(IEnumerable<int> organizationIds)
        {
            return await FindAsync(r => organizationIds.ToList().Contains(r.Id));
        }

        public async Task<List<Organization>> GetEmployeesByOrganizationAsync(List<int> organizationIDs, string active)
        {
            List<Expression<Func<QTD2.Domain.Entities.Core.Organization, bool>>> predicates = new List<Expression<Func<QTD2.Domain.Entities.Core.Organization, bool>>>();

            if (organizationIDs != null)
                predicates.Add(r => organizationIDs.Contains(r.Id));

            var organizations = await FindWithIncludeAsync(predicates, new[] { "EmployeeOrganizations.Employee.Person", "EmployeeOrganizations.Employee.EmployeePositions.Position", "EmployeeOrganizations.Employee.EmployeeCertifications.Certification" });
           
            foreach(var organization in organizations)
            {
                if (active == "Active Only")
                {
                    organization.EmployeeOrganizations = organization.EmployeeOrganizations.Where(r => r.Employee.Active).ToList();
                }

                if (active == "Inactive Only")
                {
                    organization.EmployeeOrganizations = organization.EmployeeOrganizations.Where(r => !r.Employee.Active).ToList();
                }
            }                      
            return organizations.ToList();
        }

        public async Task<List<Organization>> GetPublicOrganizationAsync()
        {
            return (await FindAsync(o => o.PublicOrganization)).ToList();
        }


    }
}
