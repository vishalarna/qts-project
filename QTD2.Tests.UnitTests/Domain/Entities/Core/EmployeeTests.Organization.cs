using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EmployeeTests
    {
        [Theory, MemberData(nameof(EmployeeTests.GetAddOrganizationData))]
        public void EmployeeTest_AddOrganization(Employee employee, Organization organization)
        {
            var orgCount = employee.EmployeeOrganizations.Count();

            employee.JoinOrganization(organization, employee.Id,false);

            Assert.Equal(orgCount + 1, employee.EmployeeOrganizations.Count());
        }

        [Theory, MemberData(nameof(EmployeeTests.GetAddOrganizationData))]
        public void EmployeeTest_LeaveOrganization(Employee employee, Organization organization)
        {
            var orgCount = employee.EmployeeOrganizations.Count();

            employee.JoinOrganization(organization, employee.Id,false);

            employee.LeaveOrganization(organization,employee.Id);

            Assert.Equal(orgCount, employee.EmployeeOrganizations.Count());
        }
    }
}

