using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class EmployeeTestData
    {
        public static List<Employee> GetAll()
        {
            return new List<Employee>()
            {
                e1(),
                e2()
            };
        }

        static Employee e1()
        {
            var employee = new Employee(1, "1","Address","City","State","1","1","WorkLocation","Notes",true, "Password",null,null,false);
            return employee;
        }

        static Employee e2()
        {
            var employee = new Employee(1, "1", "Address", "City", "State", "1", "1", "WorkLocation", "Notes", true, "Password", null, null, false);

            employee.EmployeePositions = new List<EmployeePosition>();

            return employee;
        }
    }
}
