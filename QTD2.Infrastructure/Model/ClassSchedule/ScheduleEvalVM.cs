using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ScheduleEvalVM
    {
        public string Title { get; set; }

        public bool Completed { get; set; }

        public int Id { get; set; }

        public int EmpId { get; set; }

        public bool hasAggregateData { get; set; }
    }
}
