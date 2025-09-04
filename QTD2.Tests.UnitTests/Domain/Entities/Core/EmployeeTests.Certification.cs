using QTD2.Domain.Entities.Core;
using System;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EmployeeTests
    {
        [Theory, MemberData(nameof(EmployeeTests.GetCertificationData))]
        public void EmployeeTests_AddCertification(Employee employee, Certification certification)
        {

            var certCount = employee.EmployeeCertifications.Count();

            var empCertfication = new EmployeeCertification(employee.Id, certification.Id, DateOnly.FromDateTime(DateTime.Now), null, null,null,null);

            employee.AddCertification(certification, empCertfication);

            Assert.Equal(certCount + 1, employee.EmployeeCertifications.Count());
        }

        [Theory, MemberData(nameof(EmployeeTests.GetCertificationData))]
        public void EmployeeTests_RemoveCertification(Employee employee, Certification certification)
        {

            var certCount = employee.EmployeeCertifications.Count();

            var empCertfication = new EmployeeCertification(employee.Id, certification.Id, DateOnly.FromDateTime(DateTime.Now), null, null,null,null);

            employee.AddCertification(certification, empCertfication);
            employee.DeleteCertification(certification);

            Assert.Equal(certCount, employee.EmployeeCertifications.Count());
        }
    }
}
