using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Position_Employee : Entity
    {
        public int PositionId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartTime { get; set; }

        public bool Trainee { get; set; }

        public virtual Position Position { get; set; }

        public virtual Employee Employee { get; set; }

        public Position_Employee()
        {
        }

        public Position_Employee(int positionId, int employeeId, DateTime startTime, bool trainee)
        {
            PositionId = positionId;
            EmployeeId = employeeId;
            StartTime = startTime;
            Trainee = trainee;
        }
    }
}
