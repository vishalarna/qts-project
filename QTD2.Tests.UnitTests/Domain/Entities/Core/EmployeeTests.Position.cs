using QTD2.Domain.Entities.Core;
using System;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EmployeeTests
    {
        [Theory, MemberData(nameof(EmployeeTests.GetAddPositionData))]
        public void EmployeeTests_AddPosition(Employee employee, Position position)
        {
            // this test is failing for employee1 because EmployeePositions is null
            // this should be handled
            // create a ticket in Jira for failing test
            var positionsCount = employee.EmployeePositions.Count();

            employee.AddPosition(position, employee.Id, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), true, "manager", "training program version",true);

            Assert.Equal(positionsCount + 1, employee.EmployeePositions.Count());
        }

        [Theory, MemberData(nameof(EmployeeTests.GetAddPositionData))]
        public void EmployeeTests_LeavePosition(Employee employee, Position position)
        {

            var endDate = DateOnly.FromDateTime(DateTime.Now);
            employee.AddPosition(position, employee.Id, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), true, "manager", "training program version",true);
            var empPos = employee.LeavePosition(position, endDate);

            Assert.Equal(endDate, empPos.EndDate);
        }


        [Theory, MemberData(nameof(EmployeeTests.GetAddPositionData))]
        public void EmployeeTests_QualifyPosition(Employee employee, Position position)
        {

            var qualifyDate = DateOnly.FromDateTime(DateTime.Now);
            var empPos = employee.AddPosition(position, employee.Id, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), true, "manager", "training program version",true);
            employee.Qualify(position, qualifyDate);

            Assert.Equal(qualifyDate, empPos.QualificationDate);
        }
    }
}
