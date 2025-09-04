using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class LinkEmployeeResult
    {
        public List<LinkedEmployee> LinkedEmployees { get; set; }

        public void AddLinkedEmployee(int employeeId, int classScheduleId, int classScheduleEmployeeId)
        {
            if (LinkedEmployees == null) LinkedEmployees = new List<LinkedEmployee>();

            LinkedEmployees.Add(new LinkedEmployee(employeeId, classScheduleId, classScheduleEmployeeId));
        }
    }

    public class LinkedEmployee
    {
        public int ClassScheduleEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int ClassScheduleId { get; set; }

        public LinkedEmployee() { }
        public LinkedEmployee(int employeeId, int classScheduleId, int classScheduleEmployeeId)
        {
            EmployeeId = employeeId;
            ClassScheduleId = classScheduleId;
            ClassScheduleEmployeeId = classScheduleEmployeeId;
        }
    }
}
