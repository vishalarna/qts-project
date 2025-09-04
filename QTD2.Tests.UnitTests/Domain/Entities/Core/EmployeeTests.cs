using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EmployeeTests
    {
        public static IEnumerable<object[]> GetAddPositionData()
        {
            var employees = EmployeeTestData.GetAll();
            var positions = PositionTestData.GetAll();

            var data = new List<object[]>();

            foreach (var employee in employees)
            {
                foreach (var position in positions)
                {
                    //note using deep copy to get a new instance of the mployee for fresh, predictable results each time
                    data.Add(new object[] { employee.DeepCopy(), position.DeepCopy() });
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetAddOrganizationData()
        {
            var employees = EmployeeTestData.GetAll();
            var organizations = OrganizationTestData.GetAll();

            var data = new List<object[]>();

            foreach (var employee in employees)
            {
                foreach (var organization in organizations)
                {
                    //note using deep copy to get a new instance of the mployee for fresh, predictable results each time
                    data.Add(new object[] { employee.DeepCopy(), organization.DeepCopy()});
                }
            }

            return data;
        }

        public static IEnumerable<object[]> GetCertificationData()
        {
            var employees = EmployeeTestData.GetAll();
            var certifications = CertificationTestData.GetAll();

            var data = new List<object[]>();

            foreach (var employee in employees)
            {
                foreach (var certification in certifications)
                {
                    //note using deep copy to get a new instance of the mployee for fresh, predictable results each time
                    data.Add(new object[] { employee.DeepCopy(), certification.DeepCopy() });
                }
            }

            return data;
        }
    }
}
